﻿using Microsoft.AspNetCore.Mvc;
using MyDemo.BLL.Interfaces;

namespace MyDemo.PL.Controllers
{
    //DepartmentController Here HAS 2 Relationships:
    //1.Inheritance : DepartmentController is a Controller
    //2.Assiossion[Compostion] : DepartmentController Has a DepartmentRepository [MUST]
    //*********************************************************************************

    //**************************
    //Scoped Service Life cycle:
    //**************************

    //1- Send Request => [BaseURL/Department/Index]
    //2- Because IndexAction is Non static method => must be created an object from [DepartmentController]
    //3.1- Creating a class from DepartmentController means implement the Constructor => That depends on Creation of an object of class implementing IDepartmentRepository
    //3.2- DepartmentRepository من ال oject روح اعمله IDepartmentRepository من ال object ان اي حد يحتاج CLR وانا كنت معرف ال 
    //4.1- When CLR Creates object from DepartmentRepository ,it will notice that DepartmentRepository Depends on an object from MVCDbContext
    //4.2- MVCDbContext من ال oject روح اعمله MVCDbContext من ال object ان اي حد يحتاج CLR وانا كنت معرف ال    
    //5- ClR will notice that class MVCDbContext depents on Creation an object from DbcontextOptions
    //6- ClR will Chain on the base class of DbContextOptions<MVCDbContext>
    //7-اللي عنده overrideOnCofiguring ويروح ي connectionStringمعاياال  Configureهيلاقني معرفه اني ب  
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository) //1-Ask CLR for Creating an Object from Class Implementing IDepartmentRepository
                                                                                //2-Allow dependancy into the Container of servies    
        {
            _departmentRepository = departmentRepository;
        }

        // /Department/Index
        public IActionResult Index()
        {
            return View();
        }
    }
}