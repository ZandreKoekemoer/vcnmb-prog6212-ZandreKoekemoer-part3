using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using MVCprog.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace MVCprog.Controllers
{
    public class ManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ManagerController(ApplicationDbContext context) => _context = context;

        public IActionResult FinalApproval()
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("Index", "Login");
            }

            return View(_context.Claims.Where(c => c.Status.Contains("Coordinator")).ToList());
        }

        [HttpPost]
        public IActionResult FinalApprove(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("Index", "Login");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null) { claim.Status = "Approved by Manager"; _context.SaveChanges(); }
            return RedirectToAction("FinalApproval");
        }

        [HttpPost]
        public IActionResult Reject(int id)
        {
            if (HttpContext.Session.GetString("UserRole") != "Manager")
            {
                return RedirectToAction("Index", "Login");
            }

            var claim = _context.Claims.FirstOrDefault(c => c.ClaimId == id);
            if (claim != null) { claim.Status = "Rejected by Manager"; _context.SaveChanges(); }
            return RedirectToAction("FinalApproval");
        }
    }
}


/*
 Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core (Version 2.0) [Source code].
 Available at: <https://bencull.com/blog/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core>
 [Accessed 21 November 2025].
*/
