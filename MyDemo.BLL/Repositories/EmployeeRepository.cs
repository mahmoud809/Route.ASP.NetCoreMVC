using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Data.Contexts;
using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.BLL.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MVCDemoDbContext _dbContext;

        public EmployeeRepository(MVCDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int Add(Employee employee)
        {
            _dbContext.Employees.Add(employee);
            return _dbContext.SaveChanges();
        }

        public int Delete(Employee employee)
        {
            _dbContext.Employees.Remove(employee);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Employee> GetAll()
            => _dbContext.Employees.ToList();
        

        public Employee GetById(int id)
        {
            return _dbContext.Employees.Find(id);
        }

        public int Update(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return _dbContext.SaveChanges();
        }
    }
}
