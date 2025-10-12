using WeatherPortal.Dto;

namespace WeatherPortal.Service.Interfaces
{
    public interface ITownshipService
    {
        Task Create(TownshipViewModel townshipViewModel);
        Task<IEnumerable<TownshipViewModel>> GetAll();
        Task<TownshipViewModel> GetById(string id);
        Task Delete(string twonshipId);
        Task Update(TownshipViewModel townshipViewModel);
        bool IsAlradyExist(TownshipViewModel townshipViewModel);
        Task<IEnumerable<TownshipViewModel>> GetTwonshipByCity(string cityId);
    }
}
