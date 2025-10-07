using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Data;

namespace WeatherPortal.Data.Repositories
{
    public class RegionRepository:BaseRepository<RegionEntity> ,IRegionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }
    }
}
