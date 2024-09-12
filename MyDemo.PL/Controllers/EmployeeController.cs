using Microsoft.AspNetCore.Mvc;
using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Models;

namespace MyDemo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EmployeeController(IEmployeeRepository employeeRepository , IDepartmentRepository departmentRepository) //1-Ask CLR for Creating an Object from Class Implementing IEmployeeRepository
                                                                                //2-Allow dependancy into the Container of servies    
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }

        // /Employee/Index
        public IActionResult Index()
        {
            var employees = _employeeRepository.GetAll();

            return View(employees);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Departments"] = _departmentRepository.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            //We Should make validations before submit this passing Employee => [Server side validation]
            if (ModelState.IsValid)
            {
                var recordAffected = _employeeRepository.Add(employee);
                if (recordAffected > 0)
                    return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeRepository.GetById(id.Value);
            if (employee is null)
                return NotFound();

            return View(ViewName, employee);
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
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int AffectedRow = _employeeRepository.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(employee);



        }

        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {
            if (id != employee.Id)
                return BadRequest();

            try
            {
                int AffectedRows = _employeeRepository.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(employee);
            }
        }
    }
}
