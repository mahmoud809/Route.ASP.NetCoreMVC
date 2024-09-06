using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.BLL.Interfaces
{
    public interface IEmployeeRepository
    {
        //Signature of 5 methods
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        int Add(Employee employee);
        int Update(Employee employee);
        int Delete(Employee employee);
    }
}
