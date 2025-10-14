using Microsoft.EntityFrameworkCore;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class NewsRepository:BaseRepository<NewsEntity>,INewsRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public NewsRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<NewsEntity>> GetNewsByWeatherStation(string weatherStationId)
        {
            return await _dbContext.News.Where(w => w.Id == weatherStationId).ToListAsync();
        }
    }
}
