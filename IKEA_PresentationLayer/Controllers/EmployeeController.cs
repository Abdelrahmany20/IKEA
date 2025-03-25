using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_Business_Logic_Layer.DTO_s.Employees;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using IKEA_Business_Logic_Layer.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace IKEA_PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        #region Servicse-DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion



        #region Index



        [HttpGet]




        public IActionResult Index()
        {

            var Employees = employeeServices.GetAllEmployees();
            return View(Employees);
        }
        #endregion






        #region Create



        [HttpGet]
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]

        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeDto);
            }

            try
            {
                var result = employeeServices.CreateEmployee(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error: Could not create department.");
                    return View(employeeDto);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                ModelState.AddModelError(string.Empty, "An error occurred while creating the department.");
                return View(employeeDto);
            }



        }
        #endregion


        #region Details

        [HttpGet]

        public IActionResult Details(int? id)
        {


            if (id is null)
                return BadRequest();




            var employee = employeeServices.GetEmployeeById(id.Value);



            if (employee is null)
                return NotFound();


            return View(employee);

        }




        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();


            var employee = employeeServices.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();

            var MappedEmployee = new UpdatedEmployeeDto()
            {
                Id = employee.Id,
                name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                HiringDate = employee.HiringDate,
                Salary = employee.Salary,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
                IsActive = employee.IsActive,
            };


            return View(MappedEmployee);

        }

        [HttpPost]

        public IActionResult Edit (UpdatedEmployeeDto employeeDto)

        {
            if (!ModelState.IsValid)
                return View(employeeDto);

            var Message = String.Empty;
            try
            {
                var Result = employeeServices.UpdateEmployee(employeeDto);
                if (Result > 0)
                    return RedirectToAction(nameof(Index));

                else
                    Message = "Employee is not updated";


            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception occurred in Edit POST: {Message}", ex.Message);
                Message = environment.IsDevelopment() ? ex.ToString() : "An error occurred while updating the employee.";
            }

            ModelState.AddModelError(string.Empty, Message);
            return View(employeeDto);



        }


        #endregion

        #region Delete


        [HttpGet]

        public IActionResult Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = employeeServices.GetEmployeeById(Id.Value);

            if (employee is null)
                return NotFound();


            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(int Empid)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = employeeServices.DeleteEmployee(Empid);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Employee Is Not Deleted";

            }
            catch (Exception ex)
            {
                //1.log Exceptions
                logger.LogError(ex, message);


                // 2. Set Message
                message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Delete the Employee";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { Id = Empid });
        }


        #endregion

    }
}
