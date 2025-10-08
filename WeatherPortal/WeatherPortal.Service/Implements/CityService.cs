
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class CityService:ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Create(CityViewModel cityViewModel)
        {
                var entity = new CityEntity
                {
                    Id = cityViewModel.Id,
                    CityNameInEnglish = cityViewModel.CityNameInEnglish,
                    CityNameInMyanmar = cityViewModel.CityNameInMyanmar,
                    RegionId = cityViewModel.RegionId,
                };
                await _unitOfWork.Cities.Create(entity);
                _unitOfWork.Commit();
            }

                throw new Exception("Error Occour" + e.Message) ;
            }
        }

        public async Task Delete(string cityId)
        {
            try
            {
                var existingCity = await _unitOfWork.Cities.GetBy(c => c.Id == cityId);
                var city = existingCity.FirstOrDefault();
                if (city == null)
                {
                    _unitOfWork.Cities.Delete(city);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new ("Error Occour"+ e.Message);
            }
        }

        public async Task<IEnumerable<CityViewModel>> GetAll()
        {
            var cities = (from c in _unitOfWork.Cities.GetAll().Result.ToList()
                          join r in _unitOfWork.Regions.GetAll().Result.ToList()
                          on c.RegionId equals r.Id
                          select new CityViewModel
                          {
                              Id = c.Id,
                              RegionId = r.Id,
                              CityNameInEnglish = c.CityNameInEnglish,
                              CityNameInMyanmar = c.CityNameInMyanmar,
                              
                          }).ToList();
            return cities;
                         
        }

        public async Task<CityViewModel> GetById(string id)
        {
            var cities = await _unitOfWork.Cities.GetBy(c => c.Id == id);
            return cities.Select(s => new CityViewModel
            {
                Id = s.Id,
                CityNameInEnglish = s.CityNameInEnglish,
                CityNameInMyanmar = s.CityNameInMyanmar,
                RegionId = s.RegionId,
            }).FirstOrDefault() ;
        }

        public bool IsAlradyExist(CityViewModel cityVm)
        {
            return _unitOfWork.Cities.IsAlradyExist(cityVm.CityNameInEnglish,cityVm.CityNameInMyanmar);

        }

        public async Task Update(CityViewModel cityViewModel)
        {
            var existingCities = _unitOfWork.Cities.GetBy(c => c.Id == cityViewModel.Id).Result;
            var existingCity = existingCities.FirstOrDefault();
            if (existingCity == null)
            {
                throw new Exception("City not found");
            }
            existingCity.CityNameInEnglish = cityViewModel.CityNameInEnglish;
            existingCity.CityNameInMyanmar = cityViewModel.CityNameInMyanmar;
            existingCity.RegionId = cityViewModel.RegionId;
             _unitOfWork.Cities.Update(existingCity);
            _unitOfWork.Commit();
        }
    }
}
