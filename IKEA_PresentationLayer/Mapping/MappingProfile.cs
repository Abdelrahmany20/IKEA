using AspNetCoreGeneratedDocument;
using AutoMapper;
using IKEA_Business_Logic_Layer.DTO_s;
using IKEA_Business_Logic_Layer.DTO_s.Departments;
using IKEA_PresentationLayer.ViewModel;

namespace IKEA_PresentationLayer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        
        CreateMap<DepartementViewModel, CreatedDepartmentDto>().ReverseMap();
        CreateMap<DartmentDetailsDto, DepartementViewModel>().ReverseMap();
        CreateMap<DepartementViewModel, UpdatedDepartmentDto>().ReverseMap();


        }
    }
}
