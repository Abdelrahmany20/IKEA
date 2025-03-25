using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories._Generic;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments
{
    public class DepartmentReposatory : GenericRepository<Departement>, IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DepartmentReposatory(ApplicationDbContext context) : base(context)
        {
            _dbContext = context;
        }
    

    //    private ApplicationDbContext dbcontext { get; set; }

    //    public DepartmentReposatory(ApplicationDbContext context)
    //    {
    //        dbcontext = context;
    //    }



    //    public IEnumerable<Departement> GetAll(bool withNoTracking = true)


    //    {

    //        if (withNoTracking)

    //            return dbcontext.Departements.Where(D=>D.IsDeleted== false).AsNoTracking().Where(d => d.Description != null).ToList();


    //        return dbcontext.Departements.Where(D => D.IsDeleted == false).ToList();

    //    }
    //    public Departement? GetById(int id)
    //    {

    //var department =dbcontext.Departements.Find(id);


    //        //    var department =dbcontext.Departements.Local.FirstOrDefault(d => d.Id == id);
    //        //if (department is null)

    //        //   department=dbcontext.Departements.FirstOrDefault(d => d.Id == id);


    //       return department;
    //    }
    //    public int Add(Departement department)
    //    {

    //        dbcontext.Departements.Add(department);

    //        return dbcontext.SaveChanges();
    //    }
    //    public int Update(Departement department)
    //    {
    //        dbcontext.Departements.Update(department);

    //        return dbcontext.SaveChanges();
    //    }
    //    public int Delete(Departement department)
    //    {

    //        department.IsDeleted = true;
    //        dbcontext.Departements.Update(department);

    //        return dbcontext.SaveChanges();
    //    }
    }
}
