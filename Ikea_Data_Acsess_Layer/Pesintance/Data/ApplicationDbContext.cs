using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ikea_Data_Acsess_Layer.Pesintance.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {



        }






        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Departement> Departements { get; set; }

        public DbSet<Employee> Employees { get; set; }
    }
}