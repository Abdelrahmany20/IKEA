using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using Ikea_Data_Acsess_Layer.Models.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace IKEA_PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services
        private IDepartementServices departementServices;
        private readonly ILogger<DepartmentController> logger;
        private readonly IWebHostEnvironment environment;
        private readonly IWebHostEnvironment hostEnvironment;

        public DepartmentController(IDepartementServices _departementServices, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {

            departementServices = _departementServices;
            logger = _logger;
            this.environment = environment;
        } 
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departementServices.GetAllDebartments();
            return View(Departments);

        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        #endregion





        #region Create

        [HttpPost]

        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentDto);
            }

            try
            {
                var result = departementServices.CreateDepartment(departmentDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: Could not create department.");
                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
                return View(departmentDto);
            }



        }
        #endregion




        #region Details

        [HttpGet]

        public IActionResult Details(int? id)
        {


            if( id is null)
                return BadRequest();




                var department = departementServices.GetDepartmentByiId(id.Value);



                if (department is null)
                    return NotFound();


            return View(department);

        }




        #endregion



        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
           if( id is null)
                return BadRequest();


            var department = departementServices.GetDepartmentByiId(id.Value);
            if (department is null)
                return NotFound();

            var MappedDepartment = new UpdatedDepartmentDto()
            {
                Id = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                CreationDate = department.CreationDate
            };


            return View(MappedDepartment);

        }

        [HttpPost]

        public IActionResult Edit(UpdatedDepartmentDto departmentDto)

        {
            if (!ModelState.IsValid)
                return View(departmentDto);

            var Message = String.Empty;
            try
            {
                var Result = departementServices.UpdateDepartment(departmentDto);
                if (Result>0)   
                    return RedirectToAction(nameof(Index));

                else
                    Message = "Department is not updated";


            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Exception occurred in Edit POST: {Message}", ex.Message);
                Message = environment.IsDevelopment() ? ex.ToString() : "An error occurred while updating the department.";
            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentDto);



        }


        #endregion


        #region Delete


        [HttpGet]

        public IActionResult Delete (int? id)
        {
            if (id is null)
                return BadRequest();
            var department = departementServices.GetDepartmentByiId(id.Value);
            if (department is null)
                return NotFound();
            return View(department);
        }


        [HttpPost]


        public IActionResult Delete(int DeptId)
        {
            var Message=string.Empty;
            try
            {
                var IsDeleted = departementServices.DeleteDepartment(DeptId);
                if(IsDeleted)
                    return RedirectToAction(nameof(Index));

                Message = "Department is not deleted";

            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                Message = environment.IsDevelopment() ? ex.ToString() : "An error occurred while deleting the department.";


            }


            ModelState.AddModelError(string.Empty, Message);
            return RedirectToAction(nameof(Delete), new { id= DeptId });


        }


        #endregion


    }
}
