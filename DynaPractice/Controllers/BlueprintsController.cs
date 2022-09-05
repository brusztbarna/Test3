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
using System.Security.Claims;
using DynaPractice.Dtos.BlueprintDtos;
using Microsoft.AspNetCore.Authorization;

namespace DynaPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlueprintsController : ControllerBase
    {
        private readonly MainDbContext _context;
        private readonly IBlueprintRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public BlueprintsController(MainDbContext context, IBlueprintRepository repository, IWebHostEnvironment environment)
        {
            _context = context;
            _repository = repository;
            _environment = environment;
        }

        // GET: api/Blueprints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blueprint>>> GetBlueprints()
        {
            if (_context.Blueprints == null)
            {
                return NotFound();
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(await _repository.GetBlueprintsAsync(activeUserId));
        }

        // GET: api/Blueprints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Blueprint>> GetBlueprint(int id)
        {
            if (_context.Blueprints == null)
            {
                return NotFound();
            }
            var blueprint = await _repository.GetBlueprintAsync(id);

            if (blueprint == null)
            {
                return NotFound();
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (activeUserId != blueprint.UserId)
            {
                return Unauthorized();
            }

            return blueprint;
        }

        // PUT: api/Blueprints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlueprint(int id, BlueprintRequestDto request)
        {
            var oldBlueprint = await _repository.GetBlueprintAsync(id);

            if (oldBlueprint == null) 
            {
                return BadRequest("No blueprint exist with the given id");
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (activeUserId != oldBlueprint.UserId)
            {
                return Unauthorized();
            }

            if (request == null) 
            {
                return BadRequest();
            }

            await _repository.PutBlueprintAsync(oldBlueprint, request);

            return Ok();
        }

        // POST: api/Blueprints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("uploadBlueprint")]
        public async Task<ActionResult<Blueprint>> UploadBlueprint(IFormFile file, IFormFile image, string title, string description)
        {
            if (_context.Blueprints == null)
            {
                return Problem("Entity set 'MainDbContext.Blueprints'  is null.");
            }

            if (file.Length == 0 || image.Length == 0 
                || description == null 
                || description == "" 
                || title == null 
                || title == "") 
            {
                BadRequest("Paramater is missing");
            }

            try
            {
                int userId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

                if (!Directory.Exists(_environment.WebRootPath + "\\Uploads\\" + userId + "\\"))
                {
                    Directory.CreateDirectory(_environment.WebRootPath + "\\Uploads\\" + userId + "\\");
                }

                using (FileStream imageStream = System.IO.File.Create(_environment.WebRootPath + "\\Uploads\\" + userId + "\\" + title + Path.GetExtension(image.FileName.ToString())))
                {
                    await image.CopyToAsync(imageStream);
                    imageStream.Flush();
                }

                using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Uploads\\" + userId + "\\" + title + Path.GetExtension(file.FileName.ToString())))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }

                string imgUrl = _environment.WebRootPath + "\\Uploads\\" + userId + "\\" + title + Path.GetExtension(image.FileName.ToString());
                string fileUrl = _environment.WebRootPath + "\\Uploads\\" + userId + "\\" + title + Path.GetExtension(file.FileName.ToString());

                var savedBlueprint = await _repository.PostBlueprintAsync(userId, title, description, imgUrl, fileUrl);

                if (savedBlueprint == null)
                {
                    return Problem("Problem in the saving");
                }

                return CreatedAtAction("GetBlueprint", new { id = savedBlueprint.Id }, savedBlueprint);
            }
            catch (Exception ex) 
            {
                return Problem(ex.Message);
            }
        }

        // DELETE: api/Blueprints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlueprint(int id)
        {
            if (_context.Blueprints == null)
            {
                return NotFound();
            }
            var blueprint = await _repository.GetBlueprintAsync(id);

            if (blueprint == null)
            {
                return NotFound();
            }

            int activeUserId = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (activeUserId != blueprint.UserId)
            {
                return Unauthorized();
            }

            await _repository.DeleteBlueprintAsync(blueprint);
            await _repository.SaveChangesAsync();

            return NoContent();
        }
    }
}
