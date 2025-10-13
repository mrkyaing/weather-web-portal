using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface INewsRepository:IBaseRepository<NewsEntity>
    {
        Task<IEnumerable<NewsEntity>> GetNewsByWeatherStation(string weatherStationId);
    }
}
