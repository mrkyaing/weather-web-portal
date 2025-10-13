using Microsoft.EntityFrameworkCore;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class CityRepository:BaseRepository<CityEntity>,ICityRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CityRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CityEntity>> GetCityByRegion(string regionId)
        {
            return await _dbContext.Cities.Where(c => c.RegionId == regionId).ToListAsync();
                                                                        
        }

        public async Task<CityEntity> GetCityByTownshipId(string townshipId)
        {
                return await _dbContext.Cities
           .FirstOrDefaultAsync(c => c.Id == _dbContext.Townships
               .Where(t => t.Id == townshipId)
               .Select(t => t.CityId)
               .FirstOrDefault());
        }

        public bool IsAlradyExist(string nameInEnglish, string nameInMyanmar)
        {
            return _dbContext.Cities.Where(c => c.CityNameInEnglish == nameInEnglish || c.CityNameInMyanmar == nameInMyanmar).Any();
        }
    }
}
