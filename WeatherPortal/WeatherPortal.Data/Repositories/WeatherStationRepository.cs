using Microsoft.EntityFrameworkCore;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class WeatherStationRepository : BaseRepository<WeatherStationEntity>,IWeatherStationRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WeatherStationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<WeatherStationEntity> GetCityByStation(string WeatherStationId)
            {
             return await _dbContext.WeatherStations
           .Include(w => w.City) 
           .FirstOrDefaultAsync(w => w.Id == WeatherStationId);
        }

        public bool IsAlradyExist(string StationName)
        {
            if (string.IsNullOrWhiteSpace(StationName))
                return false;

            string normalizedName = StationName.Replace(" ", "").ToLower();

            return _dbContext.WeatherStations
                .Any(x => x.StationName.Replace(" ", "").ToLower() == normalizedName);
        }
    }
}
