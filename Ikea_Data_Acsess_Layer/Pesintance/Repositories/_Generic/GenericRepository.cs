using Ikea_Data_Acsess_Layer.Models;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories._Generic
{
  public  class GenericRepository<T> :IGenericRepository<T> where T : ModelBase
    {

        private ApplicationDbContext _dbContext { get; set; }

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }



        public IEnumerable<T> GetAll(bool WithNoTracking = true)
        {
            if (WithNoTracking)
                return _dbContext.Set<T>().Where(D => D.IsDeleted == false).AsNoTracking().ToList();

            return _dbContext.Set<T>().Where(D => D.IsDeleted == false).ToList();
        }

        public T? GetById(int id)
        {
            var itme = _dbContext.Set<T>().Find(id);

            return itme;
        }

        public int Add(T Item)
        {
            _dbContext.Set<T>().Add(Item);
            return _dbContext.SaveChanges();
        }

        public int Update(T Item)
        {
            _dbContext.Set<T>().Update(Item);
            return _dbContext.SaveChanges();
        }

        public int Delete(T Item)
        {
            Item.IsDeleted = true;
            _dbContext.Set<T>().Update(Item);
            return _dbContext.SaveChanges();
        }
    }
}

