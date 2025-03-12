using Ikea_Data_Acsess_Layer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department
{
    public interface IDepartmentRepository
    {


        IEnumerable<Departement> GetAll(bool withNoTracking =true);
        Departement? GetById(int id);

        int Add(Departement department);

        int Update(Departement department);
        int Delete(Departement department);


    }
}
