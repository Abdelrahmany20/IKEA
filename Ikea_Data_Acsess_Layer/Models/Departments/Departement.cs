using Ikea_Data_Acsess_Layer.Models.Empolyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Models.Departments
{
    public class Departement : ModelBase
    {
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string? Description { get; set; }
        public DateOnly CreationDate { get; set; }



        //nav prop [many]
        public virtual ICollection<Employee>? Employees { get; set; } = new HashSet<Employee>();
    }
}
