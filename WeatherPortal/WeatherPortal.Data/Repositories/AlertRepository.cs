using WeatherPortal.Data.Data;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;

namespace WeatherPortal.Data.Repositories
{
    public class AlertRepository : BaseRepository<AlertEntity>,IAlertRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AlertRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._dbContext = dbContext;
        }

        public bool IsAlradyExist(string AlertType)
        {
            if (string.IsNullOrWhiteSpace(AlertType))
                return false;

            string normalizedName = AlertType.Replace(" ", "").ToLower();

            return _dbContext.Alerts
                .Any(x => x.AlertType.Replace(" ", "").ToLower() == normalizedName);
        }
    }
}
