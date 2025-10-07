using WeatherPortal.DataModel.DomainEntities;
namespace WeatherPortal.Service.Interfaces
{
    public interface IRegionService
    {
        Task Create(RegionEntity entity);
        Task<IEnumerable<RegionEntity>> GetAllRegions();
        Task<RegionEntity> GetRegionById(string regionId);
        Task Delete(string regionId);
        Task Update(RegionEntity entity);
        bool IsAlreadyExist(string nameInEnglish,string nameInMyanmar,int code);
    }
}
