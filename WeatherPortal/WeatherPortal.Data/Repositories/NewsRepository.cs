using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class NewsRepository:BaseRepository<NewsEntity>,INewsRepository
    {
        public NewsRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }
    }
}
