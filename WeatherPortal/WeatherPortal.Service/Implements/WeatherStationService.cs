using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class WeatherStationService :IWeatherStationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeatherStationService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task Create(WeatherStationViewModel weatherStationViewModel)
        {
            var entity = new WeatherStationEntity()
            {
                Id = Guid.NewGuid().ToString(),
               StationName= weatherStationViewModel.StationName,
               CityId= weatherStationViewModel.CityId,
               TownshipId= weatherStationViewModel.TownshipId,
               Latitude= weatherStationViewModel.Latitude,
               Longitude= weatherStationViewModel.Longitude,
               IsActive = true
            };
            await _unitOfWork.WeatherStations.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string WeatherStationId)
        {
            try
            {
                var station = _unitOfWork.WeatherStations.GetBy(w => w.Id == WeatherStationId).Result.FirstOrDefault();

                if (station != null)
                {
                    _unitOfWork.WeatherStations.Delete(station);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new("Error Occour" + e.Message);
            }
        }

        public async Task<IEnumerable<WeatherStationViewModel>> GetAll()
        {
          
            var stations = await _unitOfWork.WeatherStations.GetAll();
            var cities = await _unitOfWork.Cities.GetAll();
            var townships = await _unitOfWork.Townships.GetAll();

            
            var result = (from s in stations
                          join c in cities on s.CityId equals c.Id
                          join t in townships on s.TownshipId equals t.Id
                          select new WeatherStationViewModel
                          {
                              Id = s.Id,
                              StationName = s.StationName,
                              CityNameInEnglish = c.CityNameInEnglish,
                              TownshipNameInEnglish = t.TownshipNameInEnglish,
                              Latitude = s.Latitude,
                              Longitude = s.Longitude
                          }).ToList();

            return result;
        }


        public async Task<WeatherStationViewModel> GetById(string id)
        {
            var station = await _unitOfWork.WeatherStations.GetBy(w => w.Id == id);
            return station.Select(s => new WeatherStationViewModel
            {
                Id = s.Id,
               StationName=s.StationName,
               CityId=s.CityId,
               TownshipId=s.TownshipId,
               Latitude=s.Latitude,
               Longitude=s.Longitude
            }).SingleOrDefault();
        }

        public async Task<WeatherStationEntity> GetCityByStation(string weatherStationId)
        {
            return await _unitOfWork.WeatherStations.GetCityByStation(weatherStationId);
        }

        public bool IsAlradyExist(WeatherStationViewModel weatherStationViewModel)
        {
            return _unitOfWork.WeatherStations.IsAlradyExist(weatherStationViewModel.StationName);
        }

        public void Update(WeatherStationViewModel weatherStationViewModel)
        {
            var entity = new WeatherStationEntity()
            {
                Id = weatherStationViewModel.Id,
                StationName = weatherStationViewModel.StationName,
                CityId = weatherStationViewModel.CityId,
                TownshipId = weatherStationViewModel.TownshipId,
                Latitude = weatherStationViewModel.Latitude,
                Longitude = weatherStationViewModel.Longitude,
                UpdatedAt=DateTime.Now,
                IsActive = true
            };
            _unitOfWork.WeatherStations.Update(entity);
            _unitOfWork.Commit();
        }
    

    }
}
