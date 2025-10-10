using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface ICityService
    {
        Task Create(CityViewModel cityViewModel);
        Task<IEnumerable<CityViewModel>> GetAll();
        Task<CityViewModel> GetById(string id);
        void Delete(string cityId);
        void Update(CityViewModel cityViewModel);
        bool IsAlradyExist(CityViewModel cityVm);
        Task<IEnumerable<CityViewModel>> GetCityByRegion(string regionId);
        Task<CityEntity> GetCityByTownship(string townshipId);

    }
}
