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
        [HttpPost]
        public async Task<IActionResult> Entry(SatelliteRadarImageViewModel vm, IFormFile ImageFile)
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
                        await ImageFile.CopyToAsync(fileStream);
                    }
                    vm.ImageUrl = "/uploads/" + uniqueName;
                }
                else
                {
                    // File upload မလုပ်ရင် error ပြရန်
                    ModelState.AddModelError("ImageFile", "Please select an image file.");
                    return View(vm);
                }

                await _satelliteRadarImageService.Create(vm);
                TempData["success"] = "Satellite/Radar Image saved successfully.";
                ViewData["error"] = true;

                // Save ပြီးရင် form ကို clear လုပ်ပါ
                return RedirectToAction(nameof(Entry));
            }
            catch (Exception e)
            {
                ViewData["success"] = "Error has satellite image creating : " + e.Message;
                ViewData["error"] = false;
                return View(vm);
            }
        }
        public async Task<IActionResult> List()
        {
            try
            {
                var data = await _satelliteRadarImageService.GetAll();
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error loading images: " + ex.Message;
                return View(new List<SatelliteRadarImageViewModel>());
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _satelliteRadarImageService.Delete(id);
                TempData["success"] = "Image deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error deleting image: " + ex.Message;
            }
            return RedirectToAction("List");
        }
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    TempData["error"] = "Image ID is required";
                    return RedirectToAction("List");
                }

                var data = await _satelliteRadarImageService.GetById(id);

                if (data == null)
                {
                    TempData["error"] = "Image not found";
                    return RedirectToAction("List");
                }

                return View(data);
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error loading image: " + ex.Message;
                return RedirectToAction("List");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(SatelliteRadarImageViewModel model, IFormFile? ImageFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["error"] = "Please fix the validation errors";
                    return View("Edit", model);
                }

                // Handle file upload if new file is provided
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // Validate file type
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };
                    var fileExtension = Path.GetExtension(ImageFile.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        TempData["error"] = "Invalid file type. Please upload an image file (JPG, PNG, GIF, BMP).";
                        return View("Edit", model);
                    }

                    // Validate file size (max 5MB)
                    if (ImageFile.Length > 5 * 1024 * 1024)
                    {
                        TempData["error"] = "File size too large. Please upload an image smaller than 5MB.";
                        return View("Edit", model);
                    }

                    string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
                    Directory.CreateDirectory(uploadsFolder);

                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    model.ImageUrl = "/uploads/" + uniqueFileName;
                }

                model.UpdatedAt = DateTime.Now;
                await _satelliteRadarImageService.Update(model);

                TempData["success"] = "Image updated successfully!";
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                TempData["error"] = "Error updating image: " + ex.Message;
                return View("Edit", model);
            }
        }
    }
}
