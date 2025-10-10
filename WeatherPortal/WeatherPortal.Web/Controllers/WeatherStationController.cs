using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class WeatherStationController : Controller
    {
        private readonly IWeatherStationService _weatherStationService;
        private readonly ICityService _cityService;
        private readonly ITownshipService _townshipService;

        public WeatherStationController(IWeatherStationService weatherStationService,
                                        ICityService cityService,
                                        ITownshipService townshipService)
        {
            _weatherStationService = weatherStationService;
            _cityService = cityService;
            _townshipService = townshipService;
        }

 
        public async Task<IActionResult> Entry()
        {
            await BindCityData();
            await BindTownshipData();
            return View();
        }

        private async Task BindCityData()
        {
            var city = await _cityService.GetAll();
            ViewBag.City = city;
        }

        private async Task BindTownshipData()
        {
            var township = await _townshipService.GetAll();
            ViewBag.Township = township;
        }
       

        [HttpPost]
        public async Task<IActionResult> Entry(WeatherStationViewModel weatherStationViewModel)
        {
            try
            {
                var AlreadyExist =_weatherStationService.IsAlradyExist(weatherStationViewModel);
                if (AlreadyExist)
                {
                    ViewData["Info"] = "This  station is already exist in the system.";
                    ViewData["Status"] = false;
                    await BindCityData();
                    await BindTownshipData();
                    return View(weatherStationViewModel);
                }
                await _weatherStationService.Create(weatherStationViewModel);
                ViewData["Info"] = "This station was successfully saved in the system.";
                ViewData["Status"] = true;
            }
            catch (Exception)
            {
                ViewData["Info"] = "Error when saving data to the system.";
                ViewData["Status"] = false;
            }
            await BindCityData();
            await BindTownshipData();
            return View(weatherStationViewModel);
        }
        public async Task<IActionResult> List()
        {
            var stations = await _weatherStationService.GetAll();
            return View(stations); // Pass the IEnumerable<WeatherStationViewModel> to the view
        }
        public async Task<IActionResult> Edit(string Id)
        {
            var stations = await _weatherStationService.GetById(Id);
            await BindCityData();
            await BindTownshipData();
            return View(stations); // Pass the IEnumerable<WeatherStationViewModel> to the view
        }
        [HttpGet]
        public IActionResult Delete(string id)
        {
            try
            {
                _weatherStationService.Delete(id);
                TempData["info"] = "Weather station deleted successfully.";
                TempData["status"] = true;
            }
            catch (Exception ex)
            {
                TempData["info"] = "Error occurred while deleting: " + ex.Message;
                TempData["status"] = false;
            }

            return RedirectToAction("List"); // redirect to list page
        }
        [HttpGet]
        public async Task<JsonResult> GetTownshipsByCity(string cityId)
        {
            var townships = await _townshipService.GetTwonshipByCity(cityId);

            var result = townships.Select(t => new
            {
                Id = t.Id,
                CityId = t.CityId,
                TownshipNameInEnglish = t.TownshipNameInEnglish,
                TownshipNameInMyanmar = t.TownshipNameInMyanmar
            }).ToList(); // ← important: materialize the result

            return Json(result); // now returns proper JSON
        }



        [HttpGet]
        public async Task<JsonResult> GetCityByTownship(string townshipId)
        {
            var city = await _cityService.GetCityByTownship(townshipId);

            if (city == null)
                return Json(null);

            // Project to a new anonymous object with explicit property names
            var result = new
            {
                Id = city.Id,
                CityNameInEnglish = city.CityNameInEnglish
            };

            return Json(result); // now returns valid JSON
        }
        [HttpPost]
        public IActionResult Update(WeatherStationViewModel weatherStationViewModel)
        {
            try
            {
                var AlreadyExist = _weatherStationService.IsAlradyExist(weatherStationViewModel);
                if (AlreadyExist)
                {
                    TempData["Info"] = "This  station is already exist in the system.";
                    TempData["Status"] = false;
                     BindCityData();
                     BindTownshipData();
                    return View("Edit",weatherStationViewModel);
                }
                _weatherStationService.Update(weatherStationViewModel);
                TempData["Info"] = "This station was successfully update in the system.";
                TempData["Status"] = true;
            }
            catch (Exception)
            {
                TempData["Info"] = "Error when updating data to the system.";
                TempData["Status"] = false;
            }
            BindCityData();
            BindTownshipData();
            return RedirectToAction("List");
        }
    }
}
