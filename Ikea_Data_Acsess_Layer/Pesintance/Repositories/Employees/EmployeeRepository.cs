using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees
{
    class EmployeeRepository : IEmployeeRepository
    {
        private ApplicationDbContext dbcontext { get; set; }

        public EmployeeRepository(ApplicationDbContext context)
        {
            dbcontext = context;
        }



        public IEnumerable<Employee> GetAll(bool withNoTracking = true)


        {

            if (withNoTracking)

                return dbcontext.Employees.Where(D => D.IsDeleted == false).AsNoTracking().ToList();


            return dbcontext.Employees.Where(D => D.IsDeleted == false).ToList();

        }
        public Employee? GetById(int id)
        {

            var Employee = dbcontext.Employees.Find(id);


            //    var department =dbcontext.Departements.Local.FirstOrDefault(d => d.Id == id);
            //if (department is null)

            //   department=dbcontext.Departements.FirstOrDefault(d => d.Id == id);


            return Employee;
        }
        public int Add(Employee employee)
        {

            dbcontext.Employees.Add(employee);

            return dbcontext.SaveChanges();
        }
        public int Update(Employee employee)
        {
            dbcontext.Employees.Update(employee);

            return dbcontext.SaveChanges();
        }
        public int Delete(Employee employee)
        {

            employee.IsDeleted = true;
            dbcontext.Employees.Update(employee);

            return dbcontext.SaveChanges();
        }
    }
}
