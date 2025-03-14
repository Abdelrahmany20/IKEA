using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using Ikea_Data_Acsess_Layer.Models.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace IKEA_PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {

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

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Departments = departementServices.GetAllDebartments();
            return View(Departments);
        }
        #endregion


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



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

    }
}
