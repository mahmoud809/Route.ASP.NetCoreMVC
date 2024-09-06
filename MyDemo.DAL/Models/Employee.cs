using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required..!")] // For FrontEnd
        [MaxLength(50, ErrorMessage = "Name shouldn't be more than 50 characters..!")]
        [MinLength(3,ErrorMessage ="Name shouldn't be less than 3 characters..!")]
        public string Name { get; set; }
        [Range(22,45,ErrorMessage ="Age should be in range [22 to 45]..!")]
        public int? Age { get; set; }

        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$", ErrorMessage = "Address must be like 123-Street-City-Country")]
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name = "Hiring Date")]
        public DateTime HireDate { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;

        public bool IsActive { get; set; }
    }
}
