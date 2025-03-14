 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.DTO_s.Departments
{
   public class DartmentDto
    {

            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Code { get; set; } = null!;


        [Display(Name = "Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
