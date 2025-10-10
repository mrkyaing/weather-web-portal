using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface IWeatherStationRepository:IBaseRepository<WeatherStationEntity>
    {
        bool IsAlradyExist(string StationName);
    }
}
