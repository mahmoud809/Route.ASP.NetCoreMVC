using MyDemo.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace MyDemo.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required..!")] //For FrontEnd
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required..!")] // For FrontEnd
        public string Name { get; set; }


        //Navigational Property [Many]
        ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
