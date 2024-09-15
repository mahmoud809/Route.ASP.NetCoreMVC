﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        
        [Required] 
        public string Code { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public DateTime DateOfCreation { get; set; }

        //Navigational Property [Many]
        ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
