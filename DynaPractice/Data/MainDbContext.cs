using DynaPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace DynaPractice.Data
{
    public class MainDbContext : DbContext
    {
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Blueprint> Blueprints { get; set; }
        public DbSet<DataObject> DataObjects { get; set; }
        public DbSet<DataStructure> DataStructures { get; set; }
    }
}
