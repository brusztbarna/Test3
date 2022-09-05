using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynaPractice.Data;
using DynaPractice.Models;
using Microsoft.AspNetCore.Authorization;
using DynaPractice.Interfaces;
using DynaPractice.Dtos.DataObjectDtos;
using System.Security.Claims;

namespace DynaPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataObjectsController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IDataObjectRepository _repository;

        public DataObjectsController(MainDbContext context, IDataObjectRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/DataObjects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataObject>>> GetDataObjects()
        {
            if (_context.DataObjects == null)
            {
                return NotFound();
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _repository.GetDataObjectsAsync(activeUserId));
        }

        // GET: api/DataObjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataObject>> GetDataObject(int id)
        {
            if (_context.DataObjects == null)
            {
                return NotFound();
            }
            var dataObject = await _context.DataObjects.FindAsync(id);

            if (dataObject == null)
            {
                return NotFound();
            }

            return dataObject;
        }

        // PUT: api/DataObjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataObject(int id, DataObjectRequestDto request)
        {
            var dataObject = await _context.DataObjects.FindAsync(id);

            if (dataObject == null) 
            {
                return BadRequest();
            }

            

            dataObject.Name = request.Name;
            dataObject.Comment = request.Comment;
            dataObject.DeviceName = request.DeviceName;
            dataObject.DeviceId = request.DeviceId;
            dataObject.DataTypeId = request.DataTypeId;
            dataObject.DataTypeName = request.DataTypeName;
            dataObject.DataSample = request.DataSample;
            dataObject.Url = request.Url;
            dataObject.CallMethod = request.CallMethod;
            dataObject.Type = request.Type;

            await _context.SaveChangesAsync();

            return Ok(dataObject);
        }

        // POST: api/DataObjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataObject>> PostDataObject(DataObjectRequestDto request)
        {
            if (_context.DataObjects == null)
            {
                return Problem("Entity set 'MainDbContext.DataObjects'  is null.");
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            DataObject dataObject = new()
            {
                Name = request.Name,
                Comment = request.Comment,
                DeviceName = request.DeviceName,
                DeviceId = request.DeviceId,
                DataTypeId = request.DataTypeId,
                DataTypeName = request.DataTypeName,
                DataSample = request.DataSample,
                Url = request.Url,
                CallMethod = request.CallMethod,
                Type = request.Type,
                UserId = activeUserId
            };

            _context.DataObjects.Add(dataObject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataObject", new { id = dataObject.Id }, dataObject);
        }

        // DELETE: api/DataObjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataObject(int id)
        {
            if (_context.DataObjects == null)
            {
                return NotFound();
            }
            var dataObject = await _context.DataObjects.FindAsync(id);
            if (dataObject == null)
            {
                return NotFound();
            }

            _context.DataObjects.Remove(dataObject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
