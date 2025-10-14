using System.Runtime.CompilerServices;
using WeatherPortal.Data.Interfaces;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Service.Implements
{
    public class AlertService : IAlertService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlertService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public async Task Create(AlertViewModel alertViewModel)
        {
            var entity = new AlertEntity()
            {
                Id = Guid.NewGuid().ToString(),
                AlertType = alertViewModel.AlertType,
                Message = alertViewModel.Message,
                WeatherStationId= alertViewModel.WeatherStationId,
                CityId = alertViewModel.CityId,
                IsActive = true
            };
            await _unitOfWork.Alerts.Create(entity);
            _unitOfWork.Commit();
        }

        public void Delete(string Alertid)
        {
            try
            {
                var alert = _unitOfWork.Alerts.GetBy(w => w.Id == Alertid).Result.FirstOrDefault();

                if (alert != null)
                {
                    _unitOfWork.Alerts.Delete(alert);
                    _unitOfWork.Commit();
                }
            }
            catch (Exception e)
            {

                throw new("Error Occour" + e.Message);
            }
        }

        public async Task<IEnumerable<AlertViewModel>> GetAll()
        {
            var alerts = await _unitOfWork.Alerts.GetAll();
            var stations = await _unitOfWork.WeatherStations.GetAll();
            var cities = await _unitOfWork.Cities.GetAll();

            var result = (from a in alerts
                          join s in stations on a.WeatherStationId equals s.Id into st
                          from s in st.DefaultIfEmpty()
                          join c in cities on a.CityId equals c.Id 
                          select new AlertViewModel
                         
                          {
                              Id = a.Id,
                              AlertType = a.AlertType,
                              Message =a.Message!=null? a.Message :"N/A",
                              StationName = s != null ? s.StationName : "N/A",
                              CityNameInEnglish = c.CityNameInEnglish
                          }).ToList();

            return result;
        }

        public async Task<AlertViewModel> GetById(string id)
        {
            var station = await _unitOfWork.Alerts.GetBy(w => w.Id == id);
            return station.Select(s => new AlertViewModel
            {
                Id = s.Id,
                AlertType= s.AlertType,
                Message= s.Message,
                WeatherStationId= s.WeatherStationId,
                CityId = s.CityId,
            }).SingleOrDefault();
        }

        public bool IsAlradyExist(AlertViewModel alertViewModel)
        {
            return _unitOfWork.Alerts.IsAlradyExist(alertViewModel.AlertType);
        }

        public void Update(AlertViewModel alertViewModel)
        {
            var entity = new AlertEntity()
            {
                Id =alertViewModel.Id,
                AlertType = alertViewModel.AlertType,
                Message = alertViewModel.Message,
                WeatherStationId = alertViewModel.WeatherStationId,
                CityId = alertViewModel.CityId,
                IsActive = true,
                UpdatedAt = DateTime.Now,
            };
            _unitOfWork.Alerts.Update(entity);
            _unitOfWork.Commit();
        }
    }
}
