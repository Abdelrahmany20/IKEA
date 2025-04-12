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

        IQueryable<T> GetAll(bool WithNoTracking = true);

       Task<T>? GetById(int id);

        void Add(T Entity);
        void Update(T Entity);
        void Delete(T Entity);



    }
}
