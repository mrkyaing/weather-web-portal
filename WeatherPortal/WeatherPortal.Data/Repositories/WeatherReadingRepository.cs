using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class WeatherReadingRepository : BaseRepository<WeatherReadingEntity>, IWeatherReadingRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherReadingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }
    }
}
