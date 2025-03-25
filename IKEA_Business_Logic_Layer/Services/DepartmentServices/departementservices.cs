using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using Ikea_Data_Acsess_Layer.Models.Departments;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Department;
using Ikea_Data_Acsess_Layer.Pesintance.Repositories.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA_Business_Logic_Layer.Services.DepartmentServices
{
    public class Departementservices: IDepartementServices
    {



        private IDepartmentRepository Repository;
        public Departementservices(IDepartmentRepository _reposatory)
        {
            Repository = _reposatory;
        }





        public IEnumerable<DartmentDto> GetAllDebartments()
        {
            var Departments =Repository.GetAll().Select(dept => new DartmentDto
            {

                Id = dept.Id,
                Name = dept.Name,
                Code = dept.Code,
                CreationDate = dept.CreationDate 
            } ).ToList();

            return Departments;



            //List<DartmentDto> dartmentDtos = new List<DartmentDto>();

            //foreach (var Dept in Departments)
            //{
            //    DartmentDto dartmentDto = new DartmentDto()
            //    {
            //        Id = Dept.Id,
            //        Name = Dept.Name,
            //        Code = Dept.Code,
            //        CreationDate = Dept.CreationDate
            //    };
            //    dartmentDtos.Add(dartmentDto);
            //}


        }


        public DartmentDetailsDto? GetDepartmentByiId(int id)
        {
            var Department = Repository.GetById(id);

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


        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var CreatedDepartment =new Departement()
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

            return Repository.Add(CreatedDepartment);
        }





        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
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
            return Repository.Update(updatedDepartment);
        }




        public bool DeleteDepartment(int id)
        {
            var department = Repository.GetById(id);
            //int result = 0; 
            if (department is not null)
                return Repository.Delete(department) > 0;
            else
                return false;

        }





    }
}
 