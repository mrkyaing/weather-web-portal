using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface IRegionRepository:IBaseRepository<RegionEntity>
    {
        Task<bool> IsAlreadyExist(string nameInEnglish, string nameInMyanmar, int code);
    }
}
