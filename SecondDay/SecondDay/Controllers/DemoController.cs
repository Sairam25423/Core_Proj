using Microsoft.AspNetCore.Mvc;

namespace SecondDay.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Message = "This is the About page from DemoController";
            return View();
        }
    }
}
