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

        [HttpPost]
        public async Task<IActionResult> Entry(RegionViewModel vm)
        {
            try
            {
                await _regionService.Create(vm);
            }
            catch (Exception)
            {

                throw;
            }
          return RedirectToAction("List");
        }
        public IActionResult List()
        {
            _regionService.GetAllRegions();
            return View("List");
        }
    }
}
