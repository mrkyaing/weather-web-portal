using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class SatelliteRadarImageController : Controller
    {
        private readonly ISatelliteRadarImageService _satelliteRadarImageService;
        private readonly IWebHostEnvironment _env;

        public SatelliteRadarImageController(ISatelliteRadarImageService satelliteRadarImageService,
                                               IWebHostEnvironment env)
        {
            _satelliteRadarImageService = satelliteRadarImageService;
            _env = env;
        }

        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Entry(SatelliteRadarImageViewModel vm, IFormFile ImageFile)
        {
            try
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(fileStream);
                    }
                    vm.ImageUrl = "/uploads/" + uniqueName;
                }
                _satelliteRadarImageService.Create(vm);
                ViewData["Info"] = "Satellite/Radar Image saved successfully.";
                ViewData["Status"] = true;
            }
            catch (Exception e)
            {
                ViewData["Info"] = "Error has satellite image creating : " + e.Message;
                ViewData["Status"] = false;
            }
            return View(vm);
        }

        public IActionResult Edit(string id)
        {
            var data = _satelliteRadarImageService.GetById(id);
            return View(data);
        }

        [HttpPost]
        public IActionResult Update(SatelliteRadarImageViewModel model, IFormFile? ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(fileStream);
                }

                model.ImageUrl = "/uploads/" + uniqueFileName;
            }

            _satelliteRadarImageService.Update(model);
            return RedirectToAction("List");
        }

        public IActionResult List()
        {
            var data = _satelliteRadarImageService.GetAll();
            return View(data);
        }
        public IActionResult Delete(string id)
        {
            _satelliteRadarImageService.Delete(id);
            return RedirectToAction("List");
        }
    }
}
