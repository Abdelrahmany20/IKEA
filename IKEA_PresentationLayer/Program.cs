using IKEA_Business_Logic_Layer.Common.Services.Attachments;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using IKEA_Business_Logic_Layer.Services.EmployeeServices;
using Ikea_Data_Acsess_Layer.Models.Identity;
using Ikea_Data_Acsess_Layer.Pesintance.Data;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using Ikea_Data_Acsess_Layer.Pesintance.UnitOfWork;
using IKEA_PresentationLayer.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

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
                options.UseLazyLoadingProxies()
                       .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });






            builder.Services.AddIdentity<AplicationUser, IdentityRole>((options) =>
            {

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;//#$%
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredUniqueChars = 1;

                options.User.RequireUniqueEmail = true;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

            }).AddEntityFrameworkStores<ApplicationDbContext>();






            builder.Services.AddAuthentication().AddCookie(Options =>
            {
                Options.LogoutPath = "/Account/LogOut";
                Options.AccessDeniedPath = "/Account/Error ";
                Options.SlidingExpiration = true;
                Options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                Options.ForwardSignOut = "/Account/LogIn";
            });


            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            //builder.Services.AddScoped<IDepartmentRepository, DepartmentReposatory>();
            builder.Services.AddScoped<IDepartementServices, DepartementServices>();
            //builder.Services.AddScoped<IEmployeeRepository, EmployeeReposatory>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddAutoMapper(M=>M.AddProfile(typeof(MappingProfile)));

            builder.Services.AddScoped<IAttachmentServices, AttachmentServices>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            #endregion


             
            app.Run();
        }
    }
}
