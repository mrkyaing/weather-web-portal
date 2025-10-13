
using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface INewsService
    {
        Task Create(NewsViewModel newsViewModel);
        Task<IEnumerable<NewsViewModel>> GetAll();
        Task<NewsViewModel> GetById(string id);
        Task Delete(string id);
        Task Update(NewsViewModel newsViewModel);
        Task<IEnumerable<NewsViewModel>> GetNewsByWeatherStation(string weatherSatationId);

    }
}
