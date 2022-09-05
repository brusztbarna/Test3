using DynaPractice.Data;
using DynaPractice.Interfaces;
using DynaPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DynaPractice.Repositories
{
    public class DataStructureRepository : IDataStructureRepository
    {
        private readonly MainDbContext _context;

        public DataStructureRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<DataStructure?> GetDataStructureByDataobjectIdAsync(int deviceId, int dataTypeId)
        {
            var dataStructre = await _context.DataStructures.Where(d => d.DataObjectId == deviceId
                               && (int)d.DataType == dataTypeId)
                               .Include(d => d.DataObject)
                               .FirstOrDefaultAsync();

            return dataStructre;
        }
    }
}
