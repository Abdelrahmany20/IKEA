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


        IEnumerable<EmployeeDto> GetAllEmployees();

        EmployeeDetailsDto? GetEmployeeById(int id);

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdateEmployee(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);

    }
}
