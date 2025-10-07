using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class CityRepository:BaseRepository<CityEntity>, ICityRepository
    {
        public CityRepository(ApplicationDbContext dbContext):base(dbContext)
        {
        }
    }
}
