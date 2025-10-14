using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Implements;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class WeatherReadingController : Controller
    {
        private readonly IWeatherReadingService _weatherReadingService;
        private readonly IWeatherStationService _weatherStationService;

        public WeatherReadingController(IWeatherReadingService weatherReadingService,
                                         IWeatherStationService weatherStationService)
        {
            this._weatherReadingService = weatherReadingService;
            this._weatherStationService = weatherStationService;
        }
        public async Task<IActionResult> Entry()
        {
            await BindWeatherStation();
            return View();
        }
        private async Task BindWeatherStation()
        {
            var station = await _weatherStationService.GetAll();
            ViewBag.Station = station;
        }
        [HttpPost]
        public async Task<IActionResult> Entry(WeatherReadingViewModel weatherReadingViewModel)
        {
            try
            {
                
                await _weatherReadingService.Create(weatherReadingViewModel);
                ViewData["Info"] = "This readings was successfully saved in the system.";
                ViewData["Status"] = true;
            }
            catch (Exception)
            {
                ViewData["Info"] = "Error when saving data to the system.";
                ViewData["Status"] = false;
            }
            await BindWeatherStation();
            return View(weatherReadingViewModel);
        }
        public async Task<IActionResult> List()
        {
            var reading = await _weatherReadingService.GetAll();
            return View(reading);
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var reading = await _weatherReadingService.GetById(Id);
            await BindWeatherStation();  
            return View(reading);
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                _weatherReadingService.Delete(id);
                TempData["info"] = "Readings deleted successfully.";
                TempData["status"] = true;
            }
            catch (Exception ex)
            {
                TempData["info"] = "Error occurred while deleting: " + ex.Message;
                TempData["status"] = false;
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult Update(WeatherReadingViewModel weatherReadingViewModel)
        {
            try
            {

                _weatherReadingService.Update(weatherReadingViewModel);
                TempData["Info"] = "This Reading was successfully update in the system.";
                TempData["Status"] = true;
            }
            catch (Exception)
            {
                TempData["Info"] = "Error when updating data to the system.";
                TempData["Status"] = false;
            }
            BindWeatherStation();
           
            return RedirectToAction("List");
        }
    }
}
