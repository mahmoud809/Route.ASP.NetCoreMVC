using AutoMapper;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;

namespace MyDemo.PL.Mapping_Profiles
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}


//Life cycle of Mapping from EmployeeVM to Employee
/*
 * 1.1.EndUser => Write URL => Employee/Create => CLR Will GO and Create an object from Employee Controller.
 * 1.2.CLR Will find that the constructor need an anjection of an object of class implements "IMapper"
 * 2.CLR will go and find that we allowed dependancy Injection to addmapper.
 * 3.and I will pass thourgh it Model => Model.AddProfile(new EmployeeProfile());
 * 4.it will go and implement the constructor of employee profile then  CreateMap<EmployeeViewModel, Employee>();
 */