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

        public void Delete(string cityId)
        {
            try
            {
                var existingCity =  _unitOfWork.Cities.GetBy(c => c.Id == cityId).Result.FirstOrDefault();
                
                if (existingCity != null)
                {
                    _unitOfWork.Cities.Delete(existingCity);
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
            }).SingleOrDefault() ;
        }

        public async Task<IEnumerable<CityViewModel>> GetCityByRegion(string regionId)
        {
            var city = await Task.Run(() => _unitOfWork.Cities.GetCityByRegion(regionId)); // run background in Repository
            var result = city.Select(s => new CityViewModel  // change entity to viewModel
                                        {
                                            Id = s.Id,
                                            RegionId = s.RegionId,
                                            CityNameInEnglish= s.CityNameInEnglish,
                                            CityNameInMyanmar= s.CityNameInMyanmar
                                        }).ToList();
            return result;
        }

        public bool IsAlradyExist(CityViewModel cityVm)
        {
            return _unitOfWork.Cities.IsAlradyExist(cityVm.CityNameInEnglish,cityVm.CityNameInMyanmar);

        }

        public void Update(CityViewModel cityViewModel)
        {
            var existingCities = _unitOfWork.Cities.GetBy(c => c.Id == cityViewModel.Id).Result.FirstOrDefault();
            if (existingCities == null)
            {
                throw new Exception("City not found to update");
            }
            existingCities.CityNameInEnglish = cityViewModel.CityNameInEnglish;
            existingCities.CityNameInMyanmar = cityViewModel.CityNameInMyanmar;
            existingCities.RegionId = cityViewModel.RegionId;
            existingCities.IsActive = true;
            existingCities.UpdatedAt = DateTime.Now;
            _unitOfWork.Cities.Update(existingCities);
            _unitOfWork.Commit();
        }
    }
}
