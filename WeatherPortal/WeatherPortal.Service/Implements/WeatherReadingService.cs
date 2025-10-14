using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.UnitOfWork;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{ 
    public class WeatherReadingService:IWeatherReadingService
    {
        private readonly IUnitOfWork _unitOfWork;

        public WeatherReadingService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task Create(WeatherReadingViewModel weatherReadingViewModel)
        {
            var entity = new WeatherReadingEntity()
            {
                Id = Guid.NewGuid().ToString(),
                StationId=weatherReadingViewModel.StationId,
                WhenReadAt=weatherReadingViewModel.WhenReadAt,
                TemperatureMax=weatherReadingViewModel.TemperatureMax,
                TemperatureMin=weatherReadingViewModel.TemperatureMin,
                Pressure=weatherReadingViewModel.Pressure,
                Humidity=weatherReadingViewModel.Humidity,
                WindSpeed=weatherReadingViewModel.WindSpeed,
                WindDirection=weatherReadingViewModel.WindDirection,
                Rainfall=weatherReadingViewModel.Rainfall,
                PresentWeather=weatherReadingViewModel.PresentWeather,
                SeaWeather=weatherReadingViewModel.SeaWeather,
                IsActive = true
            };
            await _unitOfWork.weatherReadings.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string WeatherReadingid)
        {
            try
            {
                var weatherreading = _unitOfWork.weatherReadings.GetBy(w => w.Id == WeatherReadingid).Result.FirstOrDefault();


                if (weatherreading == null)
                {
                    throw new InvalidOperationException("Weather reading not found.");
                }

                _unitOfWork.weatherReadings.Delete(weatherreading);
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {

                throw new Exception("Error occurred: " + e.Message);
            }
        }

        public async Task<IEnumerable<WeatherReadingViewModel>> GetAll()
        {
            var reading = await _unitOfWork.weatherReadings.GetAll();
            var stations = await _unitOfWork.WeatherStations.GetAll();
           

            var result = (from r in reading
                          join s in stations on r.StationId equals s.Id
                          select new WeatherReadingViewModel

                          {
                              Id = r.Id,
                              StationName=s.StationName,
                              WhenReadAt=r.WhenReadAt,
                              TemperatureMax=r.TemperatureMax,
                              TemperatureMin=r.TemperatureMin,
                              Pressure = r.Pressure,
                              Humidity=r.Humidity,
                              WindSpeed= r.WindSpeed,
                              WindDirection = r.WindDirection,
                              Rainfall = r.Rainfall,
                              PresentWeather = r.PresentWeather,
                              SeaWeather = r.SeaWeather

                          }).ToList();

            return result;
        }

        public async Task<WeatherReadingViewModel> GetById(string id)
        {
            var reading = await _unitOfWork.weatherReadings.GetBy(w => w.Id == id);
            return reading.Select(s => new WeatherReadingViewModel
            {
                Id = s.Id,
               StationId=s.StationId,
               WhenReadAt=s.WhenReadAt,
               TemperatureMax=s.TemperatureMax,
               TemperatureMin=s.TemperatureMin,
               Pressure = s.Pressure,
               Humidity=s.Humidity,
               WindSpeed=s.WindSpeed,
               WindDirection=s.WindDirection,
               Rainfall = s.Rainfall,
               PresentWeather = s.PresentWeather,
               SeaWeather = s.SeaWeather
               
            }).SingleOrDefault();
        }

        public void Update(WeatherReadingViewModel weatherReadingViewModel)
        {
            var entity = new WeatherReadingEntity()
            {
                Id = weatherReadingViewModel.Id,
                StationId = weatherReadingViewModel.StationId,
                WhenReadAt = weatherReadingViewModel.WhenReadAt,
                TemperatureMax = weatherReadingViewModel.TemperatureMax,
                TemperatureMin = weatherReadingViewModel.TemperatureMin,
                Pressure = weatherReadingViewModel.Pressure,
                Humidity = weatherReadingViewModel.Humidity,
                WindSpeed = weatherReadingViewModel.WindSpeed,
                WindDirection = weatherReadingViewModel.WindDirection,
                Rainfall = weatherReadingViewModel.Rainfall,
                PresentWeather = weatherReadingViewModel.PresentWeather,
                SeaWeather = weatherReadingViewModel.SeaWeather,
                IsActive = true,
                UpdatedAt = DateTime.Now
            };
            _unitOfWork.weatherReadings.Update(entity);
            _unitOfWork.Commit();
        }
    }
}
