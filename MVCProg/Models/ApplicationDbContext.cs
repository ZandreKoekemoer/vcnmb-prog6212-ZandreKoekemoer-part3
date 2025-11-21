using Microsoft.EntityFrameworkCore;
using MVCprog.Models;

namespace MVCprog.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Reference: Ravitej Herwatta. 2024. “A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC.”  
        // According to Herwatta (2024), you can inject DbContextOptions into your DbContext class to configure connection settings.  
        // I used this reference to ensure how to create a database. 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Document> Documents { get; set; }
    }
}
/*
 Ravitej Herwatta. 2024. A Step‑by‑Step Process to Set Up a Database Connection in ASP.NET Core MVC [Source code].
 Available at: <https://medium.com/@ravitejherwatta/a-step-by-step-process-to-set-up-a-database-connection-in-asp-net-core-mvc-a03ac8b7cc04>
 [Accessed 21 November 2025].
*/
