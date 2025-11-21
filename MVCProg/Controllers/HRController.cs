using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using MVCprog.Models;
using System.Linq;

namespace MVCprog.Controllers
{
    // Reference: Ravitej Herwatta. 2024. “A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC.”
    // Herwatta (2024) explains that after configuring DbContext, you can use it in controllers to query or modify database tables.
    // I applied this in HRController to add, update, and display user data using _context.Users.

    public class HRController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HRController(ApplicationDbContext context) => _context = context;

        public IActionResult Users()
        {
            //Reference: Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core.
            // According to Ben Cull (2025), sessions in ASP.NET Core allow developers to store and access user-specific data across requests by configuring session services and using HttpContext to read and write session values.
            // I used this approach in my controller to manage user session data.
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("Index", "Login");
            }

            return View(_context.Users.ToList());
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("Index", "Login");
            }

            _context.Users.Add(user);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("Index", "Login");
            }

            return View(_context.Users.FirstOrDefault(u => u.UserId == id));
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            if (HttpContext.Session.GetString("UserRole") != "HR")
            {
                return RedirectToAction("Index", "Login");
            }

            _context.Users.Update(user);
            _context.SaveChanges();
            return RedirectToAction("Users");
        }
    }
}
/*
 Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core (Version 2.0) [Source code].
 Available at: <https://bencull.com/blog/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core>
 [Accessed 21 November 2025].

Ravitej Herwatta. 2024. A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC (Version 2.0)[Source code].
Available at: <https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04>
[Accessed 21 November 2025].

*/
