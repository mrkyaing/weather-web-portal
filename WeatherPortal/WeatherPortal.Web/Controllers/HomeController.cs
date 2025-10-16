using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WeatherPortal.Data.Data;
using WeatherPortal.Dto;

namespace WeatherPortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbcontext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbcontext)
        {
            _logger = logger;
            this._dbcontext = dbcontext;
        }

        public IActionResult Index()
        {
            var alerts = _dbcontext.Alerts
              .Where(a => a.IsActive)
              .OrderByDescending(a => a.UpdatedAt ?? a.CreatedAt)
              .Take(3)
              .Select(a => new AlertViewModel
              {
                  AlertType = a.AlertType,
                  Message = a.Message,
                  CityNameInEnglish = _dbcontext.WeatherStations
                      .Where(ws => ws.Id == a.WeatherStationId)
                      .Select(ws => ws.City.CityNameInEnglish)
                      .FirstOrDefault(),
                  StationName = _dbcontext.WeatherStations
                      .Where(ws => ws.Id == a.WeatherStationId)
                      .Select(ws => ws.StationName)
                      .FirstOrDefault()
              });

            return View(alerts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
