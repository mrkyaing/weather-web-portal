using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface ICityService
    {
        Task Create(CityViewModel cityViewModel);
        Task<IEnumerable<CityViewModel>> GetAll();
        Task<CityViewModel> GetById(string id);
        Task Delete(string cityId);
        Task Update(CityViewModel cityViewModel);
        bool IsAlradyExist(CityViewModel cityVm);
        Task<IEnumerable<CityViewModel>> GetCityByRegion(string regionId);

    }
}
