using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees
{
   public interface IEmployeeRepository
    {

        IEnumerable<Employee> GetAll(bool withNoTracking = true);
        Employee? GetById(int id);

        int Add(Employee employee);

        int Update(Employee employee);
        int Delete(Employee employee);
    }
}
