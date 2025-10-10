using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface IWeatherStationService
    {
        Task Create(WeatherStationViewModel weatherStationViewModel);
        Task<IEnumerable<WeatherStationViewModel>> GetAll();
        Task<WeatherStationViewModel> GetById(string id);
        void Delete(string WeatherStationId);
        void Update(WeatherStationViewModel weatherStationViewModel);
        bool IsAlradyExist(WeatherStationViewModel weatherStationViewModel);
       
       

    }
}
