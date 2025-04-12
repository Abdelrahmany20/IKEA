using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.UnitOfWork
{
   public interface IUnitOfWork //: IDisposable
    {






        public IDepartmentRepository DepartmentRepository { get; }

        public IEmployeeRepository employeeRepository { get; }

      public  Task<int> Complete();

        //void Dispose();






    }
}
