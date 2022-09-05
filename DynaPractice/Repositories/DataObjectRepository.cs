using DynaPractice.Data;
using DynaPractice.Interfaces;
using DynaPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DynaPractice.Repositories
{
    public class DataObjectRepository : IDataObjectRepository
    {

        private readonly MainDbContext _context;

        public DataObjectRepository(MainDbContext context)
        {
            _context = context;
        }

        public async Task<List<DataObject>> GetDataObjectsAsync(int activeuserId)
        {
            return await _context.DataObjects.Where(d => d.UserId == activeuserId).ToListAsync();
        }
    }
}
