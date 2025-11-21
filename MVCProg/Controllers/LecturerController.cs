using Microsoft.AspNetCore.Mvc;
using MVCprog.Data;
using MVCprog.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.IO;

namespace MVCprog.Controllers
{
    public class LecturerController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LecturerController(ApplicationDbContext context) => _context = context;

        public IActionResult MyClaims()
        {
            //Reference: Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core.
            // According to Ben Cull (2025), sessions in ASP.NET Core allow developers to store and access user-specific data across requests by configuring session services and using HttpContext to read and write session values.
            // I used this approach in my controller to manage user session databy storing values such as UserID and Role insid the sesion.
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("Index", "Login");
            }
            int lecturerId = HttpContext.Session.GetInt32("UserId") ?? 0;
            return View(_context.Claims.Where(c => c.LecturerId == lecturerId).ToList());
        }

        [HttpGet]
        public IActionResult SubmitClaim()
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("Index", "Login");
            }
            int lecturerId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var lecturer = _context.Users.FirstOrDefault(u => u.UserId == lecturerId);
            return View(new Claim { LecturerId = lecturer.UserId, HourlyRate = lecturer.HourlyRate });
        }

        [HttpPost]
        public IActionResult SubmitClaim(Claim claim, IFormFile supportingFile)
        {
            if (HttpContext.Session.GetString("UserRole") != "Lecturer")
            {
                return RedirectToAction("Index", "Login");
            }

            int lecturerId = HttpContext.Session.GetInt32("UserId") ?? 0;
            var lecturer = _context.Users.FirstOrDefault(u => u.UserId == lecturerId);

            claim.LecturerId = lecturer.UserId;
            claim.HourlyRate = lecturer.HourlyRate;

            if (claim.HoursWorked > 180)
            {
                ViewBag.Error = "Cannot exceed 180 hours";
                return View(claim);
            }
            if (supportingFile == null || supportingFile.Length == 0)
            {
                ViewBag.Error = "Upload a document";
                return View(claim);
            }
            var ext = Path.GetExtension(supportingFile.FileName).ToLower();
            if (ext != ".pdf" && ext != ".docx" && ext != ".xlsx")
            {
                ViewBag.Error = "Only PDF, DOCX, or XLSX files allowed";
                return View(claim);
            }
            try
            {
                // Reference: Ravitej Herwatta. 2024. “A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC.”
                // Herwatta (2024) demonstrates querying and saving data via DbContext in controllers.
                // I implemented this in LecturerController to submit claims and retrieve a lecturer's claims from the database.

                claim.DateSubmitted = DateTime.Now;
                claim.Status = "Pending";
                _context.Claims.Add(claim);
                _context.SaveChanges();
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                Directory.CreateDirectory(folder);
                var path = Path.Combine(folder, supportingFile.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                supportingFile.CopyTo(stream);

                _context.Documents.Add(new Document
                {
                    ClaimId = claim.ClaimId,
                    FileName = supportingFile.FileName,
                    FilePath = "/uploads/" + supportingFile.FileName
                });
                _context.SaveChanges();

                ViewBag.Message = "Claim submitted!";
                return View(new Claim { LecturerId = lecturer.UserId, HourlyRate = lecturer.HourlyRate });
            }
            catch
            {
                ViewBag.Error = "Error submitting claim";
                return View(claim);
            }
        }


    }
}




/*
 Ben Cull. 2025. Using Sessions and HttpContext in ASP.NET Core and MVC Core (Version 2.0) [Source code].
 Available at: <https://bencull.com/blog/using-sessions-and-httpcontext-in-aspnetcore-and-mvc-core>
 [Accessed 21 November 2025].

Ravitej Herwatta. 2024. A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC (Version 2.0) [Source code].
Available at: <https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04>
[Accessed 21 November 2025].

*/
