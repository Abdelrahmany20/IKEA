using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using IKEA_Business_Logic_Layer.Services.EmployeeServices;
using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace IKEA_PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            #region Configure Services
          
            
            builder.Services.AddControllersWithViews();




            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {

                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")) ;
            });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentReposatory>();
            builder.Services.AddScoped<IDepartementServices, Departementservices>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeReposatory>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();



            //builder.Services.AddScoped<ApplicationDbContext>();
            //builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((service) =>
            //{
            //    var optionbuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //    optionbuilder.UseSqlServer("Server=.;Database=IKEA;Trusted_Connection=True;");

            //    var options = optionbuilder.Options;
            //    return options;
            //});


            #endregion

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            #region Configure PipeLines (MiddleWares)
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion


             
            app.Run();
        }
    }
}
