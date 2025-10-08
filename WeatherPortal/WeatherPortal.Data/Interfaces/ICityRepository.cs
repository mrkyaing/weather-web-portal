using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface ICityRepository : IBaseRepository<CityEntity>
    {
        IEnumerable<CityEntity> GetCityByRegion(string regionId);
        bool IsAlradyExist(string nameInEnglish, string nameInMyanmar);
    }
}
