using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IDepartmentRepository DepartmentRepository { get; }
        public IEmployeeRepository employeeRepository { get; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            DepartmentRepository = new DepartmentReposatory(this.dbContext);
            employeeRepository = new EmployeeReposatory(this.dbContext);
        }

        public async Task<int> Complete()
        {
            return await dbContext.SaveChangesAsync();
        }

        //public void Dispose()
        //{

        //    dbContext.Dispose();
        //}


    }
}
