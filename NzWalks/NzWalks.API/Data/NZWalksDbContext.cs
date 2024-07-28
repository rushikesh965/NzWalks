using Microsoft.EntityFrameworkCore;
using NzWalks.API.Models.Domain;

namespace NzWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> options): base (options)
        {
            
        }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Difficulty> Difficulty { get; set; }
    }
}
