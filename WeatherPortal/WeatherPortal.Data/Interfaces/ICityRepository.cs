using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface ICityRepository : IBaseRepository<CityEntity>
    {
        IEnumerable<CityEntity> GetRegionByCity(string regionId);
        bool IsAlradyExist(string nameInEnglish, string nameInMyanmar);
    }
}
