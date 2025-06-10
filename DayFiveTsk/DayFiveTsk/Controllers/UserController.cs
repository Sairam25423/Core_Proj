using Microsoft.AspNetCore.Mvc;
using DayFiveTsk.Data;
using Microsoft.EntityFrameworkCore;
using DayFiveTsk.Models;

namespace DayFiveTsk.Controllers
{
    public class UserController : Controller
    {
        private readonly UserDBContext _context;
        public UserController(UserDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string search, string sortOrder)
        {
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.AgeSort = sortOrder == "Age" ? "age_desc" : "Age";

            var users = from u in _context.Users where u.IsActiveUser==true select u;

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.Name.Contains(search) || u.Email.Contains(search));
            }

            users = sortOrder switch
            {
                "name_desc" => users.OrderByDescending(u => u.Name),
                "Age" => users.OrderByDescending(u => u.Age),
                "age_desc" => users.OrderByDescending(u => u.Age),
                _ => users.OrderBy(u => u.Name),
            };
            return View(await users.ToListAsync());
        }

        public IActionResult Create() 
        { 
            return View(); 
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(User usr)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usr);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(User usr)
        {
            if (ModelState.IsValid)
            {
                _context.Update(usr);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usr);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            user.IsActiveUser = false;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
