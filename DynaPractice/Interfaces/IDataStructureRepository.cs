using DynaPractice.Models;

namespace DynaPractice.Interfaces
{
    public interface IDataStructureRepository
    {
        Task<DataStructure?> GetDataStructureByDataobjectIdAsync(int deviceId, int dataTypeId);
    }
}
