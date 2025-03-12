using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ikea_Data_Acsess_Layer.Models;

namespace Ikea_Data_Acsess_Layer.Models
{
  public  class ModelBase
    {

        public int Id { get; set; }

        public bool IsDeleted { get; set; } //soft delete 
        public int CreatedBy{ get; set; }

        public DateTime CreatedOn { get; set; }

        public int LastModifiedBy { get; set; }

        public DateTime LastModifiedOn { get; set; }


    }
}
