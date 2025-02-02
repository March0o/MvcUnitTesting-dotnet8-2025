using CsvHelper.Configuration;
using DataLayer;

namespace MvcUnitTesting_dotnet8.Models
{
    public class DepartmentMap : ClassMap<Department>
    {
        public DepartmentMap() {
            Map(m => m.Name).Index(1);
        }
    }
}
