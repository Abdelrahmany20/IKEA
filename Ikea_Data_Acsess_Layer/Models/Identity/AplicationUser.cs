using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Models.Identity
{
   public class AplicationUser  : IdentityUser
    {
         public string FirstName { get; set; } =null!;
        public string LastName { get; set; } = null!;
        public bool IsAgreed { get; set; }  


    }
}
