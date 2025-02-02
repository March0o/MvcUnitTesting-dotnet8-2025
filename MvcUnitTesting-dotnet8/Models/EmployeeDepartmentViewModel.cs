using DataLayer;

namespace MvcUnitTesting_dotnet8.Models
{
    public class EmployeeDepartmentViewModel
    {
        public List<Employee> Employees { get; set; }
        public List<Department> Departments { get; set; }
    }
}
