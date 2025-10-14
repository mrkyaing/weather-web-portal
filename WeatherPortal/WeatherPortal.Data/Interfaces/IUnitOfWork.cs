namespace WeatherPortal.Data.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRegionRepository Regions { get; }
        ICityRepository Cities { get; }
        ITownshipRepository Townships {  get; }
        IWeatherStationRepository WeatherStations { get; }
        ISatelliteRadarImageRepository SatelliteRadarImages { get; }
        INewsRepository News { get; }
        IAlertRepository Alerts { get; }
        IWeatherReadingRepository weatherReadings { get; }
        void Commit();
        void RollBack();
    }
}
