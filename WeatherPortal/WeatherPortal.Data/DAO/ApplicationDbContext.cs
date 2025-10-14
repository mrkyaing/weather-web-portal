using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WeatherPortal.DataModel.DomainEntities;
namespace WeatherPortal.Data.Data
{
    public class ApplicationDbContext: IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<TownshipEntity> Townships { get; set; }
        public DbSet<WeatherStationEntity> WeatherStations { get; set; }
        public DbSet<SatelliteRadarImageEntity> SatelliteRadarImages { get; set; }
        public DbSet<AlertEntity> Alerts { get; set; }
    }
}
