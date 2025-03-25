using Ikea_Data_Acsess_Layer.Models;
using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories._Generic
{
   public interface IGenericRepository<T> where T:ModelBase
    {

        IEnumerable<T> GetAll(bool WithNoTracking = true);

        T? GetById(int id);

        int Add(T Entity);
        int Update(T Entity);
        int Delete(T Entity);




    }
}
