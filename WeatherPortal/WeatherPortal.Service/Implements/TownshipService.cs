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

        public void Delete(string townshipId)
        {
            var existingTownship = _unitOfWork.Townships.GetBy(t => t.Id == townshipId).Result.FirstOrDefault();
            if (existingTownship != null) 
            {
                _unitOfWork.Townships.Delete(existingTownship);
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
                                TownshipNameInMyanmar = t.TownshipNameInMyanmar
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
           var townships = await Task.Run(()=> _unitOfWork.Townships.GetTownshipByCity(cityId)); // run background the repository
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

        public void Update(TownshipViewModel townshipViewModel)
        {
            var existingTownships = _unitOfWork.Townships.GetBy(t => t.Id ==  townshipViewModel.Id).Result.FirstOrDefault();
            if(existingTownships == null)
            {
                throw new Exception("Not found Township to update");
            }
            existingTownships.TownshipNameInEnglish = townshipViewModel.TownshipNameInEnglish;
            existingTownships.TownshipNameInMyanmar = townshipViewModel.TownshipNameInMyanmar;
            existingTownships.CityId = townshipViewModel.CityId;
            existingTownships.IsActive = true;
            existingTownships.UpdatedAt = DateTime.Now;
            _unitOfWork.Townships.Update(existingTownships);
            _unitOfWork.Commit();
        }
    }
}
