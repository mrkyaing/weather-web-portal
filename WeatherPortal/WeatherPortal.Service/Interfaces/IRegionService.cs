using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
namespace WeatherPortal.Service.Interfaces
{
    public interface IRegionService
    {
        Task Create(RegionViewModel regionVm);
        Task<IEnumerable<RegionViewModel>> GetAll();
        Task<RegionViewModel> GetRegionById(string regionId);
        Task Delete(string regionId);
        Task Update(RegionViewModel regionVm);
        Task<bool> IsAlreadyExist(string nameInEnglish,string nameInMyanmar,int code);
    }
}
