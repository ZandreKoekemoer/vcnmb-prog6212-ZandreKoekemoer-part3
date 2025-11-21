using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVCprog.Controllers
{
    // Reference: Hòa Nguyễn Coder (SkipperHoa). 2024. “Login and Register using ASP.NET MVC 5.” DEV Community.
    // According to Nguyễn Coder (2024), in MVC 5 one can create a User model, DbContext, and implement Login and Register actions in the controller with form validation.
    // I adapted this concept in my LoginController, to understand user authentication and session management in my MVC application.

    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoginController(ApplicationDbContext context) => _context = context;

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null)
            {
                ViewBag.Error = "Invalid email or password";
                return View();
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("UserRole", user.Role);
            HttpContext.Session.SetString("UserName", user.FullName);

            if (user.Role == "HR")
            {
                return RedirectToAction("Users", "HR");
            }

            if (user.Role == "Lecturer")
            {
                return RedirectToAction("MyClaims", "Lecturer");
            }

            if (user.Role == "Coordinator")
            {
                return RedirectToAction("VerifyClaims", "Coordinator");
            }

            if (user.Role == "Manager")
            {
                return RedirectToAction("FinalApproval", "Manager");
            }

            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}

/*
 Hòa Nguyễn Coder (SkipperHoa). 2024. Login and Register using ASP.NET MVC 5 (Version 2.0) [Source code].
 Available at: <https://dev.to/skipperhoa/login-and-register-using-asp-net-mvc-5-3i0g>
 [Accessed 21 November 2025].
*/
