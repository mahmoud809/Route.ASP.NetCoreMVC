using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Models;
using MyDemo.PL.Helpers;
using MyDemo.PL.ViewModels;
using System.Collections.Generic;

namespace MyDemo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        ///Before Using UnitOfWork
        ///private readonly IEmployeeRepository _employeeRepository;
        ///private readonly IDepartmentRepository _departmentRepository;
        

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(IUnitOfWork unitOfWork /*IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository */,IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            
            ///Before using unitofwork
            ///_employeeRepository = employeeRepository;
            ///_departmentRepository = departmentRepository;
        }

        // /Employee/Index
        public IActionResult Index(string SearchValue)
        {
            if(string.IsNullOrEmpty(SearchValue))
            {
                var employees = _unitOfWork.EmployeeRepository.GetAll();

                var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                return View(mappedEmployees);
            }
            else
            {
                var employees = _unitOfWork.EmployeeRepository.SearchEmployeeByName(SearchValue);
                var mappedEmployees = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

                return View(mappedEmployees);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _unitOfWork.DepartmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)
        {
            //We Should make validations before submit this passing Employee => [Server side validation]
            if (ModelState.IsValid)
            {
                //Mapping from EmpView to Employee to be able to store it in DB.
                employeeVM.ImageName = DocumentSettings.UplodeFile(employeeVM.Image, "images");
                var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM); //Next step : Ask Your self
                                                                                      //How can mapper map from EmpolyeeVM to Employee?
                                                                                      //We should create a profile
                _unitOfWork.EmployeeRepository.Add(mappedEmp);
                _unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeVM);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = _unitOfWork.EmployeeRepository.GetById(id.Value);
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
                    employeeVM.ImageName = DocumentSettings.UplodeFile(employeeVM.Image, "images");
                    var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                    _unitOfWork.EmployeeRepository.Update(mappedEmp);
                    _unitOfWork.Complete();
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

                _unitOfWork.EmployeeRepository.Delete(mappedEmp);
                _unitOfWork.Complete();
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
