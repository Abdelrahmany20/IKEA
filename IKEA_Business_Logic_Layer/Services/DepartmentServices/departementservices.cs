using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.DepartmentServices
{
    public class Departementservices: IDepartementServices
    {



        private DepartmentReposatory Reposatory;
        public Departementservices(DepartmentReposatory _reposatory)
        {
            Reposatory = _reposatory;
        }
    }
}
