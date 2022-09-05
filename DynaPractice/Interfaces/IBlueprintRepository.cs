using DynaPractice.Dtos.BlueprintDtos;
using DynaPractice.Models;

namespace DynaPractice.Interfaces
{
    public interface IBlueprintRepository
    {
        Task<IEnumerable<Blueprint>> GetBlueprintsAsync(int activeuserId);
        Task<Blueprint?> GetBlueprintAsync(int id);
        Task PutBlueprintAsync(Blueprint oldBlueprint, BlueprintRequestDto request);
        Task<Blueprint?> PostBlueprintAsync(int activeuserId, string title, string description, string imgUrl, string assetUrl);
        Task DeleteBlueprintAsync(Blueprint blueprint);
        Task SaveChangesAsync();
    }
}
