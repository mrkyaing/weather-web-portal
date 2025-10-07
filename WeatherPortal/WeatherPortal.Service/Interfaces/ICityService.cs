using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Service.Interfaces
{
    public interface ICityService
    {
        Task Create(CityEntity entity);
        Task<IEnumerable<CityEntity>> GetAll();
        Task<CityEntity> GetById(int id);
        Task Delete(string cityId);
        Task Update(CityEntity entity);
        bool IsAlradyExist(CityEntity entity);

    }
}
