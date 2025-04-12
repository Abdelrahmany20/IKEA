 using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_Business_Logic_Layer.DTO_s.Employees;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using IKEA_Business_Logic_Layer.Services.EmployeeServices;
using IKEA_PresentationLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using System.Threading.Tasks;

namespace IKEA_PresentationLayer.Controllers
{


    [Authorize]

    public class EmployeeController : Controller
    {
        #region Servicse-DI
        private readonly IEmployeeServices employeeServices;
        private readonly ILogger logger;
        private readonly IWebHostEnvironment environment;

        public EmployeeController(IEmployeeServices employeeServices,IDepartementServices departementServices , ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            this.employeeServices = employeeServices;
            this.logger = logger;
            this.environment = environment;
        }
        #endregion

        #region Index



        [HttpGet]




        public async Task<IActionResult> Index(string search)
        {

            var Employees = await employeeServices.GetAllEmployees( search);
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
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Create(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(employeeVM); 
            }

            var message = string.Empty;
            try
            {
                if (employeeVM.Image != null && employeeVM.Image.Length > 0)
                {
                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images");
                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    var fileName = $"{Guid.NewGuid()}_{employeeVM.Image.FileName}";
                    var filePath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        employeeVM.Image.CopyTo(stream);
                    }

                    employeeVM.ImageName = fileName;
                }

                var employeeDto = new CreatedEmployeeDto()
                {
                    name = employeeVM.name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    DepartmentId = employeeVM.DepartmentId,
                    Image = employeeVM.Image    
                };

                var result = await employeeServices.CreateEmployee(employeeDto);
                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Error: Could not create employee.";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeVM);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    message = ex.Message;
                }
                else
                {
                    message = "An Error Effect at The Creation Operator";
                }
            }

            ModelState.AddModelError(string.Empty, message);
            return View(employeeVM);
        }
        #endregion





        #region Details

        [HttpGet]
        //[Authorize(Roles = "user")]

        public async Task<IActionResult> Details(int? id)
        {


            if (id is null)
                return BadRequest();




            var employee = await employeeServices.GetEmployeeById(id.Value);



            if (employee is null)
                return NotFound();


            return View(employee);

        }




        #endregion






        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
                return BadRequest();


            var employee = await     employeeServices.GetEmployeeById(id.Value);
            if (employee is null)
                return NotFound();

            var MappedEmployee = new EmployeeViewModel()
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
                ImageName = employee.ImageName
            };




            return View(MappedEmployee);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM)
        {
            if (!ModelState.IsValid)
                return View(employeeVM);

            var Message = String.Empty;
            try
            {
                var employeeDto = new UpdatedEmployeeDto()
                {
                    Id = employeeVM.Id,
                    name = employeeVM.name,
                    Age = employeeVM.Age,
                    Address = employeeVM.Address,
                    Salary = employeeVM.Salary,
                    IsActive = employeeVM.IsActive,
                    Email = employeeVM.Email,
                    PhoneNumber = employeeVM.PhoneNumber,
                    HiringDate = employeeVM.HiringDate,
                    Gender = employeeVM.Gender,
                    EmployeeType = employeeVM.EmployeeType,
                    ImageName = employeeVM.ImageName 
                };

                var Result = await employeeServices.UpdateEmployee(employeeDto);
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
            return View(employeeVM);
        }

        #endregion






        #region Delete


        [HttpGet]

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var employee = await employeeServices.GetEmployeeById(Id.Value);

            if (employee is null)
                return NotFound();


            return View(employee);
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int Empid)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = await employeeServices.DeleteEmployee(Empid);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Employee Is Not Deleted";
            }
            catch (Exception ex)
            {
                logger.LogError(ex, message);
                message = environment.IsDevelopment() ? ex.Message : "An Error has been occurred during Delete the Employee";
            }

            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { Id = Empid });
        }


        #endregion

    }
}