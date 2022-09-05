using DynaPractice.Models;

namespace DynaPractice.Interfaces
{
    public interface IDataObjectRepository
    {
        Task<List<DataObject>> GetDataObjectsAsync(int activeUserId);
    }
}
