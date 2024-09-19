using AutoMapper;
using Microsoft.AspNetCore.Identity;
using MyDemo.PL.ViewModels;

namespace MyDemo.PL.Mapping_Profiles
{
    public class RoleProfile :Profile
    {
        public RoleProfile()
        {
            CreateMap<RoleViewModel , IdentityRole>()
                .ForMember(d => d.Name , o => o.MapFrom(s => s.RoleName))
                .ReverseMap();
        }
    }
}
