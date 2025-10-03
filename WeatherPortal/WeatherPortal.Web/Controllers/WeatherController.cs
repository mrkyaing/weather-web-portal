using Microsoft.AspNetCore.Mvc;

namespace WeatherPortal.Web.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Daily()
        {
            ViewData["Title"] = "Daily Weather";
            return View();
        }

        public IActionResult Weekly()
        {
            ViewData["Title"] = "Weekly Weather";
            return View();
        }

        public IActionResult SeaForecast()
        {
            ViewData["Title"] = "Sea Weather Forecast";
            return View();
        }
    }
}
