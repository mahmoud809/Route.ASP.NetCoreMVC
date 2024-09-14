using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;
using System.Collections.Generic;

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
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentRepository departmentRepository , IMapper mapper) //1-Ask CLR for Creating an Object from Class Implementing IDepartmentRepository
                                                                                //2-Allow dependancy into the Container of servies    
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // /Department/Index
        public IActionResult Index()
        {
            var departments = _departmentRepository.GetAll();

            var mappedDepts = _mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);
            
            return View(mappedDepts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            //We Should make validations before submit this passing department => [Server side validation]
            if(ModelState.IsValid)
            {
                var mappedDept = _mapper.Map< DepartmentViewModel , Department>(departmentVM);

                var recordAffected = _departmentRepository.Add(mappedDept);
                if(recordAffected > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(departmentVM);
        }

        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if(id is null)
                return BadRequest();

            var department = _departmentRepository.GetById(id.Value);
            if(department is null)
                return NotFound();

            var mappedDept = _mapper.Map<Department, DepartmentViewModel>(department);

            return View(ViewName, mappedDept);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return Details(id , "Edit");
            //if (id is null)
            //    return BadRequest();
            //var department = _departmentRepository.GetDepartmentById(id.Value);
            //if( department is null)
            //    return NotFound();
            //return View(department);
        }

        [HttpPost]
        public IActionResult Edit(DepartmentViewModel departmentVM , [FromRoute] int id)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var mappedDept = _mapper.Map<DepartmentViewModel, Department>(departmentVM);

                    int AffectedRow = _departmentRepository.Update(mappedDept);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(departmentVM);
            
            
            
        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {
            if (id != departmentVM.Id)
                return BadRequest();

            try
            {
                var mappedDept = _mapper.Map< DepartmentViewModel , Department>(departmentVM);

                int AffectedRows = _departmentRepository.Delete(mappedDept);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(departmentVM);
            }
        }
    }
}
