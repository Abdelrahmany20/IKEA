using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.DepartmentServices
{
    public class DepartementServices : IDepartementServices
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartementServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork; //?? throw new ArgumentNullException(nameof(unitOfWork));
        }





        public async Task<int> CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment = new Departement()
            {
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };
            unitOfWork.DepartmentRepository.Add(CreatedDepartment);

            return await unitOfWork.Complete();
            //return Repository.Add(CreatedDepartment);
        }





        public async Task<int> UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var updatedDepartment = new Departement()
            {
                Id = departmentDto.Id,
                Name = departmentDto.Name,
                Code = departmentDto.Code,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.Now
            };
            //return Repository.Update(updatedDepartment);
             unitOfWork.DepartmentRepository.Update(updatedDepartment);
            return await unitOfWork.Complete();

        }





        public async Task<bool> DeleteDepartment(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetById(id);
            //int result = 0; 
            if (department is not null)
                 unitOfWork.DepartmentRepository.Delete(department) ;
            var result = await unitOfWork.Complete();
            if ( result > 0)
                return true;
            else
                return false;
        }





        public async Task< IEnumerable<DartmentDto>> GetAllDebartments()
        {
            var Departments = await unitOfWork.DepartmentRepository.GetAll().Select(dept => new DartmentDto
            {

                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate
            }).ToListAsync();

            return Departments;

        }







        public async Task< DartmentDetailsDto>? GetDepartmentByiId(int id)
        {
            var Department = await unitOfWork.DepartmentRepository.GetById(id);

            if (Department is not null)
                return new DartmentDetailsDto()
                {
                    Id = Department.Id,
                    Name = Department.Name,
                    Code = Department.Code,
                    Description = Department.Description,
                    CreationDate = Department.CreationDate,
                    IsDeleted = Department.IsDeleted,
                    CreatedBy = Department.CreatedBy,
                    CreatedOn = Department.CreatedOn,
                    LastModifiedBy = Department.LastModifiedBy,

                };

            return null;

        }


    }
}
