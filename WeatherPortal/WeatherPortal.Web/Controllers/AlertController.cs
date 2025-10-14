using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Implements;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class AlertController : Controller
    {
        private readonly IAlertService _alertService;
        private readonly IWeatherStationService _weatherStationService;
        private readonly ICityService _cityService;

        public AlertController(IAlertService alertService,
                               IWeatherStationService weatherStationService,
                               ICityService cityService)
        {
            this._alertService = alertService;
            this._weatherStationService = weatherStationService;
            this._cityService = cityService;
        }
        public async Task<IActionResult> Entry()
        {
            await BindWeatherStation();
            await BindCityData();
            return View();
        }

        private async Task BindWeatherStation()
        {
            var station = await _weatherStationService.GetAll();
            ViewBag.Station=station;
        }
        private async Task BindCityData()
        {
            var city = await _cityService.GetAll();
            ViewBag.City = city;
        }
        [HttpGet]
        public async Task<IActionResult> GetCityByStation(string stationId)
        {
            if (string.IsNullOrEmpty(stationId))
                return BadRequest();

            var station = await _weatherStationService.GetCityByStation(stationId);
            if (station == null)
                return NotFound();

            return Json(new { cityId = station.CityId, cityName = station.City.CityNameInEnglish });
        }

        [HttpPost]
        public async Task<IActionResult> Entry(AlertViewModel alertViewModel)
        {
            try
            {
                var AlreadyExist = _alertService.IsAlradyExist(alertViewModel);
                if (AlreadyExist)
                {
                    ViewData["Info"] = "This  alerttype is already exist in the system.";
                    ViewData["Status"] = false;
                    await BindWeatherStation();
                    await BindCityData();  
                    return View(alertViewModel);
                }
                await _alertService.Create(alertViewModel);
                ViewData["Info"] = "This alerttype was successfully saved in the system.";
                ViewData["Status"] = true;
            }
            catch (Exception)
            {
                ViewData["Info"] = "Error when saving data to the system.";
                ViewData["Status"] = false;
            }
            await BindWeatherStation();
            await BindCityData();
            return View(alertViewModel);
        }
        public async Task<IActionResult> List()
        {
            var alert = await _alertService.GetAll();
            return View(alert);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var alert = await _alertService.GetById(Id);
            await BindWeatherStation();
            await BindCityData();
            return View(alert); 
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                _alertService.Delete(id);
                TempData["info"] = "Alert Type deleted successfully.";
                TempData["status"] = true;
            }
            catch (Exception ex)
            {
                TempData["info"] = "Error occurred while deleting: " + ex.Message;
                TempData["status"] = false;
            }

            return RedirectToAction("List"); // redirect to list page
        }
       
        [HttpPost]
        public IActionResult Update(AlertViewModel alertViewModel)
        {
            try
            {
               
                _alertService.Update(alertViewModel);
                TempData["Info"] = "This alert type was successfully update in the system.";
                TempData["Status"] = true;
            }
            catch (Exception)
            {
                TempData["Info"] = "Error when updating data to the system.";
                TempData["Status"] = false;
            }
            BindWeatherStation();
            BindCityData();
            return RedirectToAction("List");
        }
    }
}
