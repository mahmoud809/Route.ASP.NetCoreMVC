using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Models;
using MyDemo.PL.ViewModels;
using System.Collections.Generic;

namespace MyDemo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository ,IMapper mapper ) //1-Ask CLR for Creating an Object from Class Implementing IEmployeeRepository
                                                                                //2-Allow dependancy into the Container of servies    
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        // /Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();

            var mappedEmployees = _mapper.Map<IEnumerable<Employee> , IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmployees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //We Should make validations before submit this passing Employee => [Server side validation]
            if (ModelState.IsValid)
            {
                //Mapping from EmpView to Employee to be able to store it in DB.
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM); //Next step : Ask Your self
                                                                                      //How can mapper map from EmpolyeeVM to Employee?
                                                                                      //We should create a profile

                var recordAffected = _employeeRepository.Add(mappedEmp);
                if (recordAffected > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();
    
            var mappedEmployee = _mapper.Map<Employee,EmployeeViewModel>(employee);

            return View(ViewName, mappedEmployee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            return Details(id, "Edit");
            //if (id is null)
            //    return BadRequest();
            //var Employee = _EmployeeRepository.GetEmployeeById(id.Value);
            //if( Employee is null)
            //    return NotFound();
            //return View(Employee);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    int AffectedRow = _employeeRepository.Update(mappedEmp);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employeeVM);



        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id)
                return BadRequest();

            try
            {
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                int AffectedRows = _employeeRepository.Delete(mappedEmp);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employeeVM);
            }
        }
    }
}
