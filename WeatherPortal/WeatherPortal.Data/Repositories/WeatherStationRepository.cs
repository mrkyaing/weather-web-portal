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

        public bool IsAlradyExist(string StationName)
        {
            return _dbContext.WeatherStations.Any(x => x.StationName == StationName);
        }
    }
}
