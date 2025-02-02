using CsvHelper.Configuration;
using DataLayer;

namespace MvcUnitTesting_dotnet8.Models
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap() {
            Map(m => m.name).Index(1);
            Map(m => m.DepartmentId).Index(2);
        }
    }
}
