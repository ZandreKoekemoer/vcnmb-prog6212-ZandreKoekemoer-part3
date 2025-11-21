using Microsoft.EntityFrameworkCore;
using MVCprog.Data;

namespace MVCProg
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                // Reference: Microsoft. 2025. Working with a database in ASP.NET Core MVC.
                // According to Microsoft (2025), data can be seeded into a local database via the Program.cs using DbContext and SaveChanges().
                // I used this approach in Program.cs to seed default HR and Lecturer users into my ApplicationDbContext at startup.

                if (!context.Users.Any(u => u.Role == "HR"))
                {
                    context.Users.Add(new MVCprog.Models.User
                    {
                        FullName = "Admin",
                        Email = "hr@example.com",
                        Password = "12345678",
                        Role = "HR",
                        HourlyRate = 100
                    });
                }

                
                if (!context.Users.Any(u => u.Role == "Lecturer"))
                {
                    context.Users.Add(new MVCprog.Models.User
                    {
                        FullName = "Lecturer",
                        Email = "lecturer@example.com",
                        Password = "12345678",
                        Role = "Lecturer",
                        HourlyRate = 30
                    });
                }

                context.SaveChanges();
            }

           
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();
            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
/*
 Microsoft. 2025. Working with a database in ASP.NET Core MVC (Version 2.0) [Source code].
 Available at: <https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-10.0&tabs=visual-studio>
 [Accessed 21 November 2025].
*/
