using MyApplication.Data.Models;
using AutoMapper;
using MyApplication.Data.Entites;
using MyApplication.Models;
namespace MyApplication.Services
{
    public class MapperService : Profile
    {
        public MapperService()
        {
            ConfigureMapperEmployee();
            CreateMap<Role,RoleView>().ReverseMap();            
        }
        public void ConfigureMapperEmployee()
        {
            CreateMap<EmployeeSet,EmployeeView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EmployeeItem.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.EmployeeItem.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.EmployeeItem.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.EmployeeItem.DateOfBirth))
            .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmployeeItem.EmailId))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.EmployeeItem.MobileNo))
            .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => src.EmployeeItem.DateOfJoining))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleItem.Name))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.EmployeeItem.ProjectName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.EmployeeItem.Department))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.EmployeeItem.Location))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.EmployeeItem.ManagerName));


            CreateMap<EmployeeView, Employee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
            .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => src.DateOfJoining))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleName))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.ManagerName));

            CreateMap<Employee, EmployeeView>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
            .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => src.DateOfJoining))
            .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.RoleId))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.ManagerName));

            CreateMap<EmployeeRegistration,Employee>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => " "))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
            .ForMember(dest => dest.EmailId, opt => opt.MapFrom(src => src.EmailId))
            .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.MobileNo))
            .ForMember(dest => dest.DateOfJoining, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.RoleId, opt => opt.MapFrom(src => src.RoleName))
            .ForMember(dest => dest.ProjectName, opt => opt.MapFrom(src => src.ProjectName))
            .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department))
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location))
            .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.ManagerName));
        }

    }

}