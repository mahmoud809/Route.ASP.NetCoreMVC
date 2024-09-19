using AutoMapper;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;

namespace MyDemo.PL.Mapping_Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
