using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Implements;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;
        private readonly IRegionService _regionService;

        public CityController(ICityService cityService,IRegionService regionService)
        {
            _cityService = cityService;
            _regionService = regionService;
        }
        [HttpGet]
        public async Task<IActionResult> Entry()
        {
            await BindRegionData();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Entry(CityViewModel cityVm)
        {
            try
            {
                var existCity = _cityService.IsAlradyExist(cityVm);
                if (existCity)
                {
                    ViewData["Info"] = "City already exist";
                    ViewData["Status"] = false;
                    await BindRegionData();
                    return View(cityVm);
                }
                await _cityService.Create(cityVm);
                ViewData["Info"] = "City created successfully";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Has error in city creation" + e.Message;
                ViewData["Status"] = false;
            }
            await BindRegionData();
            return View(cityVm);
        }

        public async Task<IActionResult> List()
        {
            return View(await _cityService.GetAll());
        }

        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                var city = await _cityService.GetById(id);
                if (city == null)
                {
                    return RedirectToAction("List");
                }
                //Region dropdown ပြဖို့ Bind လုပ်
                await BindRegionData();
                return View(city);
            }
            catch (Exception)
            {
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(CityViewModel cityVm)
        {
            try
            {
                await _cityService.Update(cityVm);
                ViewData["Info"] = "city updated successfully.";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occour in updating city , " + e.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _cityService.Delete(id);
                ViewData["Info"] = "City deleted";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Has error in city deletion" + e.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }

        private async Task BindRegionData()
        {
            var regions =await _regionService.GetAll();
            ViewBag.Region = regions;
        }
    }
}
