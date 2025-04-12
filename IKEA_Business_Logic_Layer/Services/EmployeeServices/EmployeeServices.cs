using IKEA_Business_Logic_Layer.Common.Services.Attachments;
using IKEA_Business_Logic_Layer.DTO_s.Employees;
using Ikea_Data_Acsess_Layer.Models.Empolyees;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Employees;
using Ikea_Data_Acsess_Layer.Pesintance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.EmployeeServices
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IAttachmentServices atachmentServices;

        public EmployeeServices(IUnitOfWork unitOfWork,IAttachmentServices atachmentServices)
        {
            this.unitOfWork = unitOfWork;
            this.atachmentServices = atachmentServices;
        }



        public  async Task<IEnumerable<EmployeeDto>> GetAllEmployees(string search)
        {
            var employees = unitOfWork.employeeRepository.GetAll()
                              .Where(E => E.IsDeleted == false &&
                                          (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                              .Include(E => E.Department) // Include once only
                              .ToList();

            return  employees.Select(Emp => new EmployeeDto
            {
                Id = Emp.Id,
                Name = Emp.Name,
                Age = Emp.Age,
                Salary = Emp.Salary,
                IsActive = Emp.IsActive,
                Email = Emp.Email,
                Gender = Emp.Gender,
                EmployeeType = Emp.EmployeeType,
                Department = Emp.Department?.Name ?? "N/A"
            }).ToList();
        }




        public async Task<EmployeeDetailsDto>? GetEmployeeById(int id)
        {
            var employee = await unitOfWork.employeeRepository.GetById(id);
            if (employee is not null)
            {
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address,
                    IsActive = employee.IsActive,
                    Salary = employee.Salary,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                    LastModifiedBy = employee.LastModifiedBy,
                    LastModifiedOn = employee.LastModifiedOn,
                    CreatedBy = employee.CreatedBy,
                    CreatedOn = employee.CreatedOn,
                    Department = employee.Department?.Name ?? "N/A",
                    ImageName = employee.ImageName

                };
            }
            return null;
        }



        public async Task< int> CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                CreatedBy = 1,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                CreatedOn = DateTime.Now,


            };


            if (employeeDto.Image is not null)
            {
                employee.ImageName = atachmentServices.UploadImage(employeeDto.Image,"images");
                
            }



            unitOfWork.employeeRepository.Add(employee);
            return await unitOfWork.Complete();
        }




        public async Task<bool> DeleteEmployee(int id)
        {

            var employee = await unitOfWork.employeeRepository.GetById(id);




            //int result = 0; 
            if (employee is not null)

            {

                if(employee.ImageName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot","files","images", employee.ImageName);

                    atachmentServices.Delete(filePath);
                }
                //return unitOfWork.Complete() ;
            }
            unitOfWork.employeeRepository.Delete(employee);

            var result = await unitOfWork.Complete();
            if( result > 0)
                return true;

            else
                return false;
        }



        public async Task<int> UpdateEmployee(UpdatedEmployeeDto employeeDto) 
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.name,
                Age = employeeDto.Age,
                Address = employeeDto.Address,
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                DepartmentId = employeeDto.DepartmentId,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now,
                ImageName = employeeDto.ImageName,

            };

            if (employeeDto.Image is not null)
            {
                if(employeeDto.ImageName is not null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", "images", employeeDto.ImageName);
                    atachmentServices.Delete(filePath);
                }
                employee.ImageName = atachmentServices.UploadImage(employeeDto.Image, "images");
            }
            unitOfWork.employeeRepository.Update(employee);
            return await unitOfWork.Complete();
        }
    }
}