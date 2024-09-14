using AutoMapper;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;

namespace MyDemo.PL.Mapping_Profiles
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<DepartmentViewModel, Department>().ReverseMap();
        }
    }
}
