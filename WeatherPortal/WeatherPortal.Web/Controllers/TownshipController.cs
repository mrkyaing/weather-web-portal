using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class TownshipController : Controller
    {
        private readonly ITownshipService _townshipService;
        private readonly ICityService _cityService;

        public TownshipController(ITownshipService townshipService,ICityService cityService)
        {
            _townshipService = townshipService;
            _cityService = cityService;
        }
        [HttpGet]
        public async Task<IActionResult> Entry()
        {
            await BindCityData();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Entry(TownshipViewModel townshipVm)
        {
            try
            {
                var isExist = _townshipService.IsAlradyExist(townshipVm);
                if (isExist)
                {
                    ViewData["Info"] = "Township already exist";
                    ViewData["Status"] = false;
                    await BindCityData();
                    return View(townshipVm);
                }
                await _townshipService.Create(townshipVm);
                ViewData["Info"] = "Township created successfully";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error has Townhip creating";
                ViewData["Status"] = false;
            }
            await BindCityData();
            return View(townshipVm);
        }
        public async Task<IActionResult> List()
        {
            return View( await _townshipService.GetAll());
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var township = await _townshipService.GetById(id);
                if (township == null)
                {
                    return RedirectToAction("List");
                }
                await BindCityData();
                return View(township);
            }
            catch (Exception e)
            {
                return RedirectToAction("List");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(TownshipViewModel townshipVm)
        {
            try
            {
                await _townshipService.Update(townshipVm);
                TempData["Info"] = "Township updated successfully.";
                TempData["Status"] = true;
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                TempData["Info"] = "Can not update township: " + e.Message;
                TempData["Status"] = false;
                return RedirectToAction("List");
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _townshipService.Delete(id);
                ViewData["Info"] = "Township deleted successfully.";
                ViewData["Status"] = true;
                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Can not delete township : " + e.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
        private async Task BindCityData()
        {
            var cities = await _cityService.GetAll();
            ViewBag.Cities = cities;
        }
    }
}
