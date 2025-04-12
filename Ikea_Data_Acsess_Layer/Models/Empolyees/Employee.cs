using Ikea_Data_Acsess_Layer.Commons.Enums;
using Ikea_Data_Acsess_Layer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Models.Empolyees
{
    public class Employee : ModelBase
    {

        public string Name { get; set; }
        public int? Age { get; set; }
        public decimal Salary { get; set; }
        public string? Address { get; set; }

        public bool IsActive { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }
        public DateOnly HiringDate { get; set; }

        public Gender Gender { get; set; }

        public EmployeeType EmployeeType { get; set; }




        public int? DepartmentId { get; set; } //FK [one to many]

        //nav prop [one]
        public virtual Departement? Department { get; set; }

        public string? ImageName { get; set; }


    }
}
