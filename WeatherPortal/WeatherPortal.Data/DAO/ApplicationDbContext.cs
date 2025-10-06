using Microsoft.EntityFrameworkCore;
using WeatherPortal.Core.DomainEntities;

namespace WeatherPortal.Data.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<TownshipEntity> Townships { get; set; }
    }
}
