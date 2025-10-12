using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Service.Interfaces;
using WeatherPortal.Dto;

namespace WeatherPortal.Web.Controllers
{
    public class RegionController : Controller
    {
        private readonly IRegionService _regionService;

        public RegionController(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public async Task<IActionResult> Entry()
        {
            //var model = new RegionPageViewModel
            //{
            //    Region = new RegionViewModel(),
            //    RegionList = await _regionService.GetAllRegions() ?? new List<RegionViewModel>()
            //};
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entry(RegionViewModel vm)
        {
            //var model = new RegionPageViewModel 
            //{ 

            //    Region = vm,
            //    RegionList = await _regionService.GetAllRegions() ?? new List<RegionViewModel>()
            //};
            try
            {
                var isExist = await _regionService.IsAlreadyExist(vm.RegionNameInEnglish, vm.RegionNameInMyanmar, vm.OrderCode);
                if (isExist)
                {
                    ViewData["Info"] = "Region already exists.";
                    ViewData["Status"] = false;
                    return View(vm);
                }
                await _regionService.Create(vm);
                ViewData["Info"] = "Region created successfully.";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occour in creating region" + e.Message;
                ViewData["Status"] = false;
            }
          return View(vm);
        }
        public async Task<IActionResult> List()
        {
            return View(await _regionService.GetAll());
        }

        public async Task<IActionResult> Edit(string id)
        {
            var region = await _regionService.GetRegionById(id);
            if (region == null)
            {
                ViewData["Info"] = "Region not found.";
                ViewData["Status"] = false;
                return RedirectToAction("List");
            }
            return View(region);
        }
        [HttpPost]
        public async Task<IActionResult> Update(RegionViewModel vm)
        {
            try
            {
                await _regionService.Update(vm);
                ViewData["Info"] = "Region updated successfully.";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occour in updating region , " + e.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _regionService.Delete(id);
                ViewData["Info"] = "Region deleted successfully.";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error occour in deleting region , " + e.Message;
                ViewData["Status"] = false;
            }
            return RedirectToAction("List");
        }
    }
}

