using Microsoft.AspNetCore.Mvc;
using WeatherPortal.DataModel.DomainEntities;
using WeatherPortal.Service.Implements;
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

        public async Task<IActionResult> Entry(RegionViewModel vm)
        {
            var regionEntity = new RegionEntity
            {
                Id = Guid.NewGuid().ToString(),
                RegionNameInEnglish = vm.RegionNameInEnglish,
                RegionNameInMyanmar = vm.RegionNameInMyanmar,
                Code = vm.Code,
                RegionType = vm.RegionType, // Division(D) or State(S)
            };
            await _regionService.Create(regionEntity);
            return RedirectToAction("List");
        }
    }
}
