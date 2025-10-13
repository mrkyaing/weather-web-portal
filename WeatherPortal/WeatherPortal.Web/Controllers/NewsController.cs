using Microsoft.AspNetCore.Mvc;
using WeatherPortal.Dto;
using WeatherPortal.Service.Interfaces;

namespace WeatherPortal.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IWeatherStationService _weatherStationService;

        public NewsController(INewsService newsService, IWeatherStationService weatherStationService)
        {
            _newsService = newsService;
            _weatherStationService = weatherStationService;
        }

        public async Task<IActionResult> Entry()
        {
            try
            {
                ViewBag.WeatherStation = await _weatherStationService.GetAll();
                return View(new NewsViewModel
                {
                    PublishedAt = DateTime.Now,
                    IsPublic = true
                });
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error loading weather stations: " + ex.Message;
                TempData["Status"] = false;
                return View(new NewsViewModel());
            }
        }

        [HttpPost]
        public async Task<IActionResult> Entry(NewsViewModel newsViewModel)
        {
            try
            {
                ViewBag.WeatherStation = await _weatherStationService.GetAll();

                // Clear ModelState errors for non-required fields
                ModelState.Remove("Id");
                ModelState.Remove("WeatherStationName");
                ModelState.Remove("UpdatedAt");

                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();

                    TempData["Info"] = "Please fix the validation errors: " + string.Join(", ", errors);
                    TempData["Status"] = false;
                    return View(newsViewModel);
                }

                await _newsService.Create(newsViewModel);
                TempData["Info"] = "News created successfully";
                TempData["Status"] = true;

                // Clear form after successful submission
                ModelState.Clear();
                return View(new NewsViewModel
                {
                    PublishedAt = DateTime.Now,
                    IsPublic = true
                });
            }
            catch (Exception e)
            {
                TempData["Info"] = "Error creating news: " + e.Message;
                TempData["Status"] = false;
                return View(newsViewModel);
            }
        }
        public async Task<IActionResult> List()
        {
            try
            {
                var news = await _newsService.GetAll();
                return View(news);
            }
            catch (Exception ex)
            {
                TempData["Info"] = "Error loading news: " + ex.Message;
                TempData["Status"] = false;
                return View(new List<NewsViewModel>());
            }
        }
    }
}