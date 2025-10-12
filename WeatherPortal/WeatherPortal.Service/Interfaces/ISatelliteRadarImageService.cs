using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface ISatelliteRadarImageService
    {
        Task<IEnumerable<SatelliteRadarImageViewModel>> GetAll();
        Task<SatelliteRadarImageViewModel> GetById(string id);
        Task Create(SatelliteRadarImageViewModel vm);
        Task Update(SatelliteRadarImageViewModel vm);
        Task Delete(string id);
    }
}
