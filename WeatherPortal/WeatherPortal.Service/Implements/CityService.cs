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
                    Id = Guid.NewGuid().ToString(),
                    CityNameInEnglish = cityViewModel.CityNameInEnglish,
                    CityNameInMyanmar = cityViewModel.CityNameInMyanmar,
                    RegionId = cityViewModel.RegionId,
                    IsActive = true
                };
                await _unitOfWork.Cities.Create(entity);
                _unitOfWork.Commit();
        }

        public async Task Delete(string cityId)
        {
            try
            {
                var existingCity = await _unitOfWork.Cities.GetBy(c => c.Id == cityId);
                var existingCityResult = existingCity.SingleOrDefault();
                if (existingCityResult != null)
                {
                    _unitOfWork.Cities.Delete(existingCityResult);
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
            var cities = (from c in await _unitOfWork.Cities.GetAll()
                          join r in await _unitOfWork.Regions.GetAll()
                          on c.RegionId equals r.Id
                          select new CityViewModel
                          {
                              Id = c.Id,
                              RegionId = r.Id,
                              CityNameInEnglish = c.CityNameInEnglish,
                              CityNameInMyanmar = c.CityNameInMyanmar,
                              RegionNameInEnglish = r.RegionNameInEnglish,
                              RegionNameInMyanmar = r.RegionNameInMyanmar
                              
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
            }).SingleOrDefault() ;
        }

        public async Task<IEnumerable<CityViewModel>> GetCityByRegion(string regionId)
        {
            var city = await _unitOfWork.Cities.GetCityByRegion(regionId); 
            var result = city.Select(s => new CityViewModel  
                                        {
                                            Id = s.Id,
                                            RegionId = s.RegionId,
                                            CityNameInEnglish= s.CityNameInEnglish,
                                            CityNameInMyanmar= s.CityNameInMyanmar
                                        }).ToList();
            return result;
        }

        public async Task<CityEntity> GetCityByTownship(string townshipId)
        {
            return await _unitOfWork.Cities.GetCityByTownshipId(townshipId);
        }

        public bool IsAlradyExist(CityViewModel cityVm)
        {
            return _unitOfWork.Cities.IsAlradyExist(cityVm.CityNameInEnglish,cityVm.CityNameInMyanmar);

        }

        public async Task Update(CityViewModel cityViewModel)
        {
            var existingCities = await _unitOfWork.Cities.GetBy(c => c.Id == cityViewModel.Id);
            var existingCity = existingCities.FirstOrDefault();
            if (existingCity == null)
            {
                throw new Exception("City not found to update");
            }
            existingCity.CityNameInEnglish = cityViewModel.CityNameInEnglish;
            existingCity.CityNameInMyanmar = cityViewModel.CityNameInMyanmar;
            existingCity.RegionId = cityViewModel.RegionId;
            existingCity.IsActive = true;
            existingCity.UpdatedAt = DateTime.Now;
            _unitOfWork.Cities.Update(existingCity);
            _unitOfWork.Commit();
        }
    }
}
