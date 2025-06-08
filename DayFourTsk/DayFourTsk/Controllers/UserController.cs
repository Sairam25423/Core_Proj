using DayFourTsk.DataAccess;
using DayFourTsk.Models;
using Microsoft.AspNetCore.Mvc;

namespace DayFourTsk.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDAL userdal;
        public UserController(UserDAL dal)
        {
            userdal = dal ?? throw new ArgumentNullException(nameof(dal), "UserDAL cannot be null.");
        }
        public IActionResult Index()
        {
            var users = userdal.GetAllUsers();
            return View(users);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                userdal.AddUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        public IActionResult Edit(int Id)
        {
            var user=userdal.GetUser(Id);
            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                userdal.UpdateUser(user);
            }
            return View(user);
        }
        public IActionResult Delete(int Id)
        {
            var user = userdal.GetUser(Id);
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            userdal.DeleteUser(Id);
            return RedirectToAction("Index");
        }
        public IActionResult Details(int Id)
        {
            var user = userdal.GetUser(Id);
            return View(user);
        }
    }
}
