using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DynaPractice.Data;
using DynaPractice.Models;
using DynaPractice.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DynaPractice.Dtos.DataStructureDtos;
using System.Security.Claims;

namespace DynaPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DataStructuresController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IDataStructureRepository _repository;

        public DataStructuresController(MainDbContext context, IDataStructureRepository repository)
        {
            _context = context;
            _repository = repository;
        }

        // GET: api/DataStructures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataStructure>>> GetDataStructures()
        {
          if (_context.DataStructures == null)
          {
              return NotFound();
          }
            return await _context.DataStructures.ToListAsync();
        }

        //  /api/DataObjects/device/{deviceId}/data-type/{dataTypeId}
        [HttpGet("device/{deviceId}/data-type/{dataTypeId}", Name = "GetDataObjectWithStructureByIdAsync")]
        public async Task<ActionResult<DataStructure>> GetDataObjectWithStructureByIdAsync(int deviceId, int dataTypeId)
        {
            var dataStructure = await _repository.GetDataStructureByDataobjectIdAsync(deviceId, dataTypeId);

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (dataStructure == null)
            {
                return NotFound();
            }
            else if(dataStructure.DataObject.UserId != activeUserId)
            {
                return Unauthorized();
            }
            else
            {
                return Ok(dataStructure);
            }
        }

        // GET: api/DataStructures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataStructure>> GetDataStructure(int id)
        {
          if (_context.DataStructures == null)
          {
              return NotFound();
          }
            var dataStructure = await _context.DataStructures.FindAsync(id);

            if (dataStructure == null)
            {
                return NotFound();
            }

            return dataStructure;
        }

        // PUT: api/DataStructures/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDataStructure(int id, DataStructureRequestDto request)
        {
            if (_context.DataStructures == null)
            {
                return NotFound();
            }

            var dataStructure = await _context.DataStructures.FindAsync(id);

            if (dataStructure == null) 
            {
                return BadRequest();
            }

            dataStructure.DataType = request.DataType;
            dataStructure.DataValue = request.DataValue;
            dataStructure.DataMask = request.DataMask;
            dataStructure.DataUnit = request.DataUnit;
            dataStructure.DataUnitPosition = request.DataUnitPosition;
            dataStructure.DataLevel = request.DataLevel;
            dataStructure.DataObjectId  = request.DataObjectId;

            await _context.SaveChangesAsync();

            return Ok(dataStructure);
        }

        // POST: api/DataStructures
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataStructure>> PostDataStructure(DataStructureRequestDto request)
        {
          if (_context.DataStructures == null)
          {
              return Problem("Entity set 'MainDbContext.DataStructures'  is null.");
          }

            DataStructure dataStructure = new()
            {
                DataType = request.DataType,
                DataValue = request.DataValue,
                DataMask = request.DataMask,
                DataUnit = request.DataUnit,
                DataUnitPosition = request.DataUnitPosition,
                DataLevel = request.DataLevel,
                DataObjectId = request.DataObjectId
            };

            _context.DataStructures.Add(dataStructure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDataStructure", new { id = dataStructure.Id }, dataStructure);
        }

        // DELETE: api/DataStructures/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataStructure(int id)
        {
            if (_context.DataStructures == null)
            {
                return NotFound();
            }
            var dataStructure = await _context.DataStructures.FindAsync(id);
            if (dataStructure == null)
            {
                return NotFound();
            }

            _context.DataStructures.Remove(dataStructure);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
