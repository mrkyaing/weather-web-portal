using WeatherPortal.DataModel.DomainEntities;
namespace WeatherPortal.Service.Interfaces
{
    public interface IRegionService
    {
        Task Create(RegionEntity entity);
        Task<IEnumerable<RegionEntity>> GetAllRegions();
        Task<RegionEntity> GetRegionById(string regionId);
        Task Delete(string regionId);
        Task Update(RegionEntity regionViewModel);
        bool IsAlreadyExist(RegionEntity regionViewModel);
    }
}
