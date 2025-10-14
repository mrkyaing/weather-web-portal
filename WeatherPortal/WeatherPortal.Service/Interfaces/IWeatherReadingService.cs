using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface IWeatherReadingService
    {
        Task Create(WeatherReadingViewModel weatherReadingViewModel);
        Task<IEnumerable<WeatherReadingViewModel>> GetAll();
        Task<WeatherReadingViewModel> GetById(string id);
        void Delete(string WeatherReadingid);
        void Update(WeatherReadingViewModel weatherReadingViewModel);
    }
}
