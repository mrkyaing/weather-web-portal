using WeatherPortal.Data.Interfaces;
using WeatherPortal.Data.Data;
using WeatherPortal.Data.Repositories;

namespace WeatherPortal.Data.UnitOfWork
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IRegionRepository Regions { get; }
        public ICityRepository Cities { get; }
        public ITownshipRepository Townships { get; set; }
        public ISatelliteRadarImageRepository SatelliteRadarImages { get; set; }

        private IWeatherStationRepository _weatherStationRepository;
        public IWeatherStationRepository WeatherStations
        {
            get
            {
                return _weatherStationRepository = _weatherStationRepository ?? new WeatherStationRepository(_dbContext);
            }
        }
        private IAlertRepository _alertRepository;
        public IAlertRepository Alerts
        {
            get
            {
                return _alertRepository=_alertRepository ?? new AlertRepository(_dbContext);
            }
        }
        private IWeatherReadingRepository _weatherReadingRepository;
        public IWeatherReadingRepository weatherReadings
        {
            get
            {
                return _weatherReadingRepository=_weatherReadingRepository ?? new WeatherReadingRepository(_dbContext);
            }
        }
        public UnitOfWork(ApplicationDbContext dbContext,
                          IRegionRepository regionRepository,
                          ICityRepository cityRepository ,
                          ITownshipRepository townshipRepository,
                          ISatelliteRadarImageRepository satelliteRadarImageRepository
            //IWeatherStationRepository weatherStationRepository
            ) 
       
        {
            _dbContext = dbContext;
            Regions = regionRepository;
            Cities = cityRepository;
            Townships = townshipRepository;
            SatelliteRadarImages = satelliteRadarImageRepository;
            //WeatherStations = weatherStationRepository;
        }
        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void RollBack()
        {
            _dbContext.Database.CurrentTransaction?.Rollback();
        }
    }
}
