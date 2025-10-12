using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WeatherPortal.Data.Repositories
{
    public class RegionRepository:BaseRepository<RegionEntity> ,IRegionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public RegionRepository(ApplicationDbContext dbContext):base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<bool> IsAlreadyExist(string nameInEnglish, string nameInMyanmar, int code)
        {
            return await _dbContext.Regions.AnyAsync(r => r.RegionNameInEnglish == nameInEnglish 
                                            || r.RegionNameInMyanmar == nameInMyanmar 
                                            || r.OrderCode == code);
        }
    }
}
