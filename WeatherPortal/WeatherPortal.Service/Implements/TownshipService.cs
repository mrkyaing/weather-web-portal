using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class TownshipService : ITownshipService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TownshipService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task Create(TownshipViewModel townshipViewModel)
        {
            var entity = new TownshipEntity
            {
                Id = Guid.NewGuid().ToString(),
                CityId = townshipViewModel.CityId,
                TownshipNameInEnglish = townshipViewModel.TownshipNameInEnglish,
                TownshipNameInMyanmar = townshipViewModel.TownshipNameInMyanmar,
                IsActive = true
            };
            await _unitOfWork.Townships.Create(entity);
            _unitOfWork.Commit();
        }

        public async Task Delete(string townshipId)
        {
            var existingTownship = await _unitOfWork.Townships.GetBy(t => t.Id == townshipId);
            var existingTownshipResult = existingTownship.SingleOrDefault();
            if (existingTownshipResult != null) 
            {
                _unitOfWork.Townships.Delete(existingTownshipResult);
                _unitOfWork.Commit();
            }
        }

        public async Task<IEnumerable<TownshipViewModel>> GetAll()
        {
            var township = (from t in _unitOfWork.Townships.GetAll().Result.ToList()
                            join c in _unitOfWork.Cities.GetAll().Result.ToList()
                            on t.CityId equals c.Id                            
                            select new TownshipViewModel
                            {
                                Id = t.Id,
                                CityId = c.Id,
                                TownshipNameInEnglish = t.TownshipNameInEnglish,
                                TownshipNameInMyanmar = t.TownshipNameInMyanmar,
                                CityNameInEnglish = c.CityNameInEnglish,
                                CityNameInMyanmar = c.CityNameInMyanmar
                            }).ToList();
            return township;
        }

        public async Task<TownshipViewModel> GetById(string id)
        {
            var townships = await _unitOfWork.Townships.GetBy(t => t.Id == id);
            return townships.Select(t => new TownshipViewModel 
                                            {
                                                 Id = t.Id,
                                                 CityId = t.CityId,
                                                 TownshipNameInEnglish = t.TownshipNameInEnglish,
                                                 TownshipNameInMyanmar = t.TownshipNameInMyanmar
                                            }).SingleOrDefault();
        }
        public async Task<IEnumerable<TownshipViewModel>> GetTwonshipByCity(string cityId)
        {
            var townships = await _unitOfWork.Townships.GetTownshipByCity(cityId); 
            var result = townships.Select(t => new TownshipViewModel
                                            {
                                                Id = t.Id,
                                                CityId = t.CityId,
                                                TownshipNameInEnglish= t.TownshipNameInEnglish,
                                                TownshipNameInMyanmar = t.TownshipNameInMyanmar
                                            }).ToList();
            return result;
        }

        public bool IsAlradyExist(TownshipViewModel townshipViewModel)
        {
            return _unitOfWork.Townships.IsExistingTownship(townshipViewModel.TownshipNameInEnglish,townshipViewModel.TownshipNameInMyanmar);
        }

        public async Task Update(TownshipViewModel townshipViewModel)
        {
            var existingTownships = await _unitOfWork.Townships.GetBy(t => t.Id ==  townshipViewModel.Id);
            var existingTownship = existingTownships.FirstOrDefault();
            if (existingTownships == null)
            {
                throw new Exception("Not found Township to update");
            }
            existingTownship.TownshipNameInEnglish = townshipViewModel.TownshipNameInEnglish;
            existingTownship.TownshipNameInMyanmar = townshipViewModel.TownshipNameInMyanmar;
            existingTownship.CityId = townshipViewModel.CityId;
            existingTownship.IsActive = true;
            existingTownship.UpdatedAt = DateTime.Now;
            _unitOfWork.Townships.Update(existingTownship);
            _unitOfWork.Commit();
        }
    }
}
