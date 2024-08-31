using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Models
{
    public class Department
    {
        /*
         * elsa7 eny mktb4 hena "Data Annotation" bs hn3dla b3deen
         */
        public int Id { get; set; }
        [Required(ErrorMessage = "Code is required..!")] //For FrontEnd
        public string Code { get; set; }
        [Required(ErrorMessage ="Name is required..!")] // For FrontEnd
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
