using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;
using Tracker.WebAPIClient;
using Week2Lab12025.Models;

namespace MvcUnitTesting_dotnet8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ActivityAPIClient.Track(StudentID: "S00234148", StudentName: "Martynas", activityName: "Rad302 2025 Week 2 Lab 1", Task: "Implementing Production Repository Pattern");


            // Add services to the container.
            builder.Services.AddControllersWithViews();
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<BookDbContext>(options =>
                options.UseSqlServer(connectionString));
            // Register the repository as a service
            builder.Services.AddScoped<IRepository<Book>, WorkingBookRepository<Book>>();

            var employeeDepartmentConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                                        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<EmployeeDepartmentContext>(options =>
               //New Target assembly directive for migrations
               options.UseSqlServer(employeeDepartmentConnectionString, b => b.MigrationsAssembly("MvcUnitTesting-dotnet8")));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            using (var scope = app.Services.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                var hostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                DbSeeder dbSeeder = new DbSeeder(_ctx, hostEnvironment);
                dbSeeder.Seed();
            }

            using (var scope = app.Services.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetRequiredService<EmployeeDepartmentContext>();
                var hostEnvironment = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                _ctx.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Employees ON");

                _ctx.Seed(hostEnvironment.ContentRootPath);
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
