using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Models
{
    //POCO Class that be stored in database So We just need to write properties and annotations that only mapped to DB.
    //And anything else releted to EndUser We will show it through ViewModel.
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
       
        public int? Age { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }

        public int? DepartmentId { get; set; }

        //Navigational Property [One]
        public Department Department { get; set; }
    }
}
