using DayThreeVal.Models;
using Microsoft.AspNetCore.Mvc;

namespace DayThreeVal.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user) {
            if (ModelState.IsValid)
            {
                ViewBag.Message = $"Welcome {user.Name}, Age:{user.Age}, Email:{user.Email}";
                return View("Success");
            }
            return View(user);
        }
        public IActionResult Success()
        {
            return View();
        }
    }
}
