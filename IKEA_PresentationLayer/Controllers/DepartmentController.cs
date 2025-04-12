using AutoMapper;
using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_Business_Logic_Layer.Services.DepartmentServices;
using Ikea_Data_Acsess_Layer.Models.Departments;
using IKEA_PresentationLayer.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IKEA_PresentationLayer.Controllers
{


    [Authorize]

    public class DepartmentController : Controller
    {
        private readonly IDepartementServices departementServices;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> logger;

        private readonly IWebHostEnvironment environment;

        public DepartmentController(IDepartementServices _departementService,IMapper mapper, ILogger<DepartmentController> _logger, IWebHostEnvironment environment)
        {
            departementServices = _departementService;
            this.mapper = mapper;
            logger = _logger;
            this.environment = environment;
        }
        #region Index

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var Departments = await departementServices.GetAllDebartments();

            return View(Departments);  
        }
        #endregion


        #region Details
        [HttpGet]
        public async Task<IActionResult> Details(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var department = await departementServices.GetDepartmentByiId(Id.Value);
            if (department is null)
                return NotFound();
            return View(department);

        }


        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Create(DepartementViewModel departmentVM)
        {
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }
            var Message = string.Empty;
            try
            {
                var departmentDto=mapper.Map<DepartementViewModel, CreatedDepartmentDto>(departmentVM);
                //var departmentDto = new CreatedDepartmentDto()
                //{
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    Description = departmentVM.Description,
                //    CreationDate = DateOnly.FromDateTime(DateTime.Now),
                //};
                var Result = await departementServices.CreateDepartment(departmentDto);

                if (Result > 0)

                {
                    TempData["Message"] = $"{departmentDto.Name}  Department Created Successfully";

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    Message = "Department is not Created";
                    ModelState.AddModelError(string.Empty, Message);
                    return View(departmentDto);

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                if (environment.IsDevelopment())
                {
                    Message = ex.Message;
                }
                else
                {
                    Message = "An Error Effect at The Creation Operator";
                }
            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);




        }

        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department = await departementServices.GetDepartmentByiId(Id.Value);

            if (Department is null)
                return NotFound();


            var MappedDepartment=mapper.Map<DartmentDetailsDto, DepartementViewModel>(Department);
            //var MappedDepartment = new DepartementViewModel()
            //{
            //    Id = Department.Id,
            //    Name = Department.Name,
            //    Code = Department.Code,
            //    Description = Department.Description,
            //    CreationDate = Department.CreationDate,

            //};

            return View(MappedDepartment);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(DepartementViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);
            var Message = string.Empty;
            try
            {
                var departmentDto=mapper.Map<DepartementViewModel, UpdatedDepartmentDto>(departmentVM);
                //var departmentDto = new UpdatedDepartmentDto()
                //{
                //    Id = departmentVM.Id,
                //    Name = departmentVM.Name,
                //    Code = departmentVM.Code,
                //    CreationDate = departmentVM.CreationDate,
                //    Description = departmentVM.Description,
                //};
                var Result = await departementServices.UpdateDepartment(departmentDto);

                if (Result > 0)
                    return RedirectToAction(nameof(Index));
                else
                    Message = "Department is Not Updated";
            }
            catch (Exception ex)
            {

                //1.log Exceptions
                logger.LogError(ex, Message);


                // 2. Set Message
                Message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Update the Department";

            }

            ModelState.AddModelError(string.Empty, Message);
            return View(departmentVM);
        }

        #endregion


        #region Delete
        [HttpGet]

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id is null)
                return BadRequest();

            var Department = await departementServices.GetDepartmentByiId(Id.Value);

            if (Department is null)
                return NotFound();


            return View(Department);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int DeptId)
        {
            var message = string.Empty;
            try
            {
                var IsDeleted = await departementServices.DeleteDepartment(DeptId);
                if (IsDeleted)
                    return RedirectToAction(nameof(Index));

                message = "Department Is Not Deleted";

            }
            catch (Exception ex)
            {
                //1.log Exceptions
                logger.LogError(ex, message);


                // 2. Set Message
                message = environment.IsDevelopment() ? ex.Message : "An Error has been occured during Delete the Department";

            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Delete), new { Id = DeptId });
        }

        #endregion
    }

}
