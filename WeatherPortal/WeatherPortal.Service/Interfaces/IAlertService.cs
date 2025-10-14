using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface IAlertService
    {
        Task Create(AlertViewModel alertViewModel);
        Task<IEnumerable<AlertViewModel>> GetAll();
        Task<AlertViewModel> GetById(string id);
        void Delete(string Alertid);
        void Update(AlertViewModel alertViewModel);
        bool IsAlradyExist(AlertViewModel alertViewModel);


    }
}
