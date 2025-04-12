using IKEA_Business_Logic_Layer.DTO_s.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.EmployeeServices
{
    public interface IEmployeeServices
    {


       Task< IEnumerable<EmployeeDto>> GetAllEmployees(string search);


        Task< EmployeeDetailsDto>? GetEmployeeById(int id);

        Task<int> CreateEmployee(CreatedEmployeeDto employeeDto);

        Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto);

        Task<bool> DeleteEmployee(int id);

    }
}