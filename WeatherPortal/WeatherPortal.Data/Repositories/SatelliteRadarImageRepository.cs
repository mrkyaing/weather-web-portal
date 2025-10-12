using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class SatelliteRadarImageRepository : BaseRepository<SatelliteRadarImageEntity>, ISatelliteRadarImageRepository
    {
        public SatelliteRadarImageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
 
