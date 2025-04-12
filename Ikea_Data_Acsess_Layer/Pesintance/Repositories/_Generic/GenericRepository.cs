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



        public IQueryable<T> GetAll(bool WithNoTracking = true)
        {
            var query = _dbContext.Set<T>().AsQueryable();

            if (typeof(T).GetProperty("IsDeleted") != null)
            {
                query = query.Where(x => !EF.Property<bool>(x, "IsDeleted")); 
            }

            if (WithNoTracking)
                return query.AsNoTracking();

            return query;
        }

        public async Task<T?> GetById(int id)
        {
            var itme = await _dbContext.Set<T>().FindAsync(id);

            return itme;
        }

        public void Add(T Item)
        {
            _dbContext.Set<T>().Add(Item);
            //return _dbContext.SaveChanges();
        }

        public void Update(T Item)
        {
            _dbContext.Set<T>().Update(Item);
            //return _dbContext.SaveChanges();
        }

        public void Delete(T item)
        {
            var propertyInfo = typeof(T).GetProperty("IsDeleted");

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(item, true);

                _dbContext.Set<T>().Update(item);
                //return _dbContext.SaveChanges();
            }

            _dbContext.Set<T>().Remove(item);
            //return _dbContext.SaveChanges();
        }
    }
}


