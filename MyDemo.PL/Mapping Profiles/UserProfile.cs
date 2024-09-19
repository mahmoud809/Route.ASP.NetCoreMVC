using AutoMapper;
using MyDemo.DAL.Models;

namespace MyDemo.PL.Mapping_Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUser>().ReverseMap();
        }
    }
}
