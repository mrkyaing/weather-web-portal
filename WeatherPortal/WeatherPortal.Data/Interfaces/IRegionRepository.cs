using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface IRegionRepository:IBaseRepository<RegionEntity>
    {
        bool IsAlreadyExist(int code, string NameInEnglish, string NameInMyanmar);
    }
}
