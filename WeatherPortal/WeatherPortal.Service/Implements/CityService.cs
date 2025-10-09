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

        public async Task Update(CityViewModel cityViewModel)
        {
            var existingCities = await _unitOfWork.Cities.GetBy(c => c.Id == cityViewModel.Id);
            var existingCitie = existingCities.FirstOrDefault();
            if (existingCitie == null)
            {
                throw new Exception("City not found to update");
            }
            existingCitie.CityNameInEnglish = cityViewModel.CityNameInEnglish;
            existingCitie.CityNameInMyanmar = cityViewModel.CityNameInMyanmar;
            existingCitie.RegionId = cityViewModel.RegionId;
            existingCitie.IsActive = true;
            existingCitie.UpdatedAt = DateTime.Now;
            _unitOfWork.Cities.Update(existingCitie);
            _unitOfWork.Commit();
        }
    }
}
