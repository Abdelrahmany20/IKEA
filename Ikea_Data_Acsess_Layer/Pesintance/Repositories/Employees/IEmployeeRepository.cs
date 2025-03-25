using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees
{
   public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //IEnumerable<Employees> GetAll(bool WithNoTracking = true);

        //Employees? GetById(int id);

        //int Add(Employees employees);
        //int Update(Employees employees);
        //int Delete(Employees employees);



    }
}
