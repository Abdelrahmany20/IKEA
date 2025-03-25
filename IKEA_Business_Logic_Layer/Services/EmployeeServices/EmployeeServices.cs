using IKEA_Business_Logic_Layer.DTO_s.Employees;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IEmployeeRepository reposatory;
        public EmployeeServices(IEmployeeRepository employeeReposatory)
        {
            reposatory = employeeReposatory;
        }

        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            return reposatory.GetAll().Where(E => E.IsDeleted == false).Select(Emp => new EmployeeDto
            {

                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Salary = Emp.Salary,
                IsActive = Emp.IsActive,
                Email = Emp.Email,
                Gender = Emp.Gender,
                EmployeeType =Emp.EmployeeType
            }).ToList();

        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = reposatory.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                };
            }
            return null;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                CreatedOn = DateTime.Now,

            };
            return reposatory.Add(employee);
        }

        public bool DeleteEmployee(int id)
        {

            var employee = reposatory.GetById(id);
            //int result = 0; 
            if (employee is not null)
                return reposatory.Delete(employee) > 0;
            else
                return false;
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,

            };
            return reposatory.Update(employee);
        }
    }
}
