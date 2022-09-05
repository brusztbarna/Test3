using DynaPractice.Data;
using DynaPractice.Dtos.BlueprintDtos;
using DynaPractice.Interfaces;
using DynaPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DynaPractice.Repositories
{
    public class BlueprintRepository : IBlueprintRepository
    {
        private readonly MainDbContext _context; 
        public BlueprintRepository(MainDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Blueprint>> GetBlueprintsAsync(int activeuserId)
        {
            return await _context.Blueprints.Where(b => b.UserId == activeuserId).ToListAsync();
        }

        public async Task<Blueprint?> GetBlueprintAsync(int id)
        {
            return await _context.Blueprints.FindAsync(id);
        }

        public async Task PutBlueprintAsync(Blueprint oldBlueprint, BlueprintRequestDto request)
        {
            oldBlueprint.Title = request.Title;
            oldBlueprint.Description = request.Description;
            oldBlueprint.ImgUrl = request.ImgUrl;
            oldBlueprint.AssetUrl = request.AssetUrl;

            await _context.SaveChangesAsync();
        }

        public async Task<Blueprint?> PostBlueprintAsync(int activeuserId, string title, string description, string imgUrl, string assetUrl)
        {
            Blueprint blueprint = new Blueprint
            {
                Title = title,
                Description = description,
                ImgUrl = imgUrl,
                AssetUrl = assetUrl,
                UserId = activeuserId
            };

            _context.Blueprints.Add(blueprint);
            await _context.SaveChangesAsync();

            return blueprint;
        }

        public async Task DeleteBlueprintAsync(Blueprint blueprint)
        {
            _context.Blueprints.Remove(blueprint);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
