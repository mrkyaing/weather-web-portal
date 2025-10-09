using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface ITownshipRepository:IBaseRepository<TownshipEntity>
    {
        IEnumerable<TownshipEntity> GetTownships();
        bool IsExistingTownship(string townshipNameInEnglish,string townshipNameInMyanmar);
        IEnumerable<TownshipEntity> GetTownshipByCity(string cityId);
    }
}
