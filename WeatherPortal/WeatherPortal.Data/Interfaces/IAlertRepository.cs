using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Interfaces
{
    public interface IAlertRepository:IBaseRepository<AlertEntity>
    {
        bool IsAlradyExist(string AlertType);
    }
}
