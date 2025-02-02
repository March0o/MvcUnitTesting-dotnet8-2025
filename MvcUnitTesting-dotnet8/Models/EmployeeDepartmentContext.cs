using DataLayer;
using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Data;
using MvcUnitTesting_dotnet8.Models;

namespace MvcUnitTesting_dotnet8.Models
{
    public class EmployeeDepartmentContext : DbContext
    {
        public EmployeeDepartmentContext() :base() { }
        public EmployeeDepartmentContext(DbContextOptions<EmployeeDepartmentContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public void Seed(string contentRootPath)
        {
            if (!this.Departments.Any())
            {
                var filepath = Path.Combine(contentRootPath, "Data\\Departments.csv");
                var departments = DbHelper.GetFile<Department, DepartmentMap>(filepath);
                this.Departments.AddRange(departments);
                this.SaveChanges();
            }
            if (!this.Employees.Any())
            {
                var filepath = Path.Combine(contentRootPath, "Data\\Employee.csv");
                var employees = DbHelper.GetFile<Employee, EmployeeMap>(filepath);
                this.Employees.AddRange(employees);
                this.SaveChanges();
            }
        }
    }
}
