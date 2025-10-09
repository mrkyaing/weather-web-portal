using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface ITownshipService
    {
        Task Create(TownshipViewModel townshipViewModel);
        Task<IEnumerable<TownshipViewModel>> GetAll();
        Task<TownshipViewModel> GetById(string id);
        void Delete(string twonshipId);
        void Update(TownshipViewModel townshipViewModel);
        bool IsAlradyExist(TownshipViewModel townshipViewModel);
        Task<IEnumerable<TownshipViewModel>> GetTwonshipByCity(string cityId);
    }
}
