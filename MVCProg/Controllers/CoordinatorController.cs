using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using MVCprog.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVCprog.Controllers
{
    public class CoordinatorController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CoordinatorController(ApplicationDbContext context) => _context = context;

        public IActionResult VerifyClaims()
        {
            //Reference: Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core.
            // According to Ben Cull (2025), sessions in ASP.NET Core allow developers to store and access user-specific data across requests by configuring session services and using HttpContext to read and write session values.
            // I used this approach in my controller to manage user session data and maintain login state by storing values such as UserID and Role inside the session.

            if (HttpContext.Session.GetString("UserRole") != "Coordinator")
            {
                return RedirectToAction("Index", "Login");
            }

            return View(_context.Claims.Where(c => c.Status == "Pending").ToList());
        }
        // Reference: Ravitej Herwatta. 2024. “A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC.”
        // Herwatta (2024) shows that you can access and modify database records using DbContext in controllers.
        // Used in CoordinatorController to approve or reject claims.

        [HttpPost]
        public IActionResult Approve(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Coordinator")
            {
                return RedirectToAction("Index", "Login");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null)
            {
                return RedirectToAction("VerifyClaims");
            }
            claim.Status = "Approved by Coordinator"; _context.SaveChanges(); return RedirectToAction("VerifyClaims");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Coordinator")
            {
                return RedirectToAction("Index", "Login");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim == null)
            {
                return RedirectToAction("VerifyClaims");
            }
            claim.Status = "Rejected by Coordinator"; _context.SaveChanges(); return RedirectToAction("VerifyClaims");
        }
    }
}




/*

Ravitej Herwatta. 2024. A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC (Version 2.0) [Source code].
Available at: <https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04>
[Accessed 21 November 2025].


Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core (Version 2.0) [Source code].
 Available at: <https://bencull.com/blog/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core>
 [Accessed 21 November 2025].
*/
