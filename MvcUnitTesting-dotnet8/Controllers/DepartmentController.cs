using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcUnitTesting_dotnet8.Models;

public class DepartmentController : Controller
{
    private readonly EmployeeDepartmentContext _context;

    public DepartmentController(EmployeeDepartmentContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var employees = _context.Employees
            .OrderBy(e => e.DepartmentId)
            .ToList();

        var departments = _context.Departments.ToList();

        var viewModel = new EmployeeDepartmentViewModel
        {
            Employees = employees,
            Departments = departments
        };

        return View(viewModel); // This will link to a View named "Index"
    }
}
