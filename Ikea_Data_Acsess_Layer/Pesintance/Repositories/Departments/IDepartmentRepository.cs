using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories._Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department
{
    public interface IDepartmentRepository :IGenericRepository<Departement>
    {
        //IEnumerable<Department> GetAll(bool WithNoTracking = true);

        //Department? GetById(int id);

        //int Add(Department department);
        //int Update(Department department);
        //int Delete(Department department);


    }
}
