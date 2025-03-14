using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using Ikea_Data_Acsess_Layer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.DepartmentServices
{
   public  interface IDepartementServices
    {

        //DTO   Data Transfer Object
        IEnumerable<DartmentDto> GetAllDebartments();

        DartmentDetailsDto? GetDepartmentByiId (int id);

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment( UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);


    }
}
