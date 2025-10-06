namespace WeatherPortal.Data.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRegionRepository Regions { get; }
        void Commit();
        void RollBack();
    }
}
