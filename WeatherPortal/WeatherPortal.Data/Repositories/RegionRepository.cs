using WeatherPortal.Core.DomainEntities;
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

        // unique code and name
        public bool IsAlreadyExist(int code, string NameInEnglish, string NameInMyanmar)
        {
            return _dbContext.Regions.Where(r => r.Code == code &&
                                                 r.RegionNameInEnglish == NameInEnglish && 
                                                 r.RegionNameInMyanmar == NameInMyanmar)
                                     .Any();
        }
    }
}
