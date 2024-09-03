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
    public class DepartmentRepository : IDepartmentRepository
    {

        private readonly  MVCDemoDbContext _dbContext;
        public DepartmentRepository(MVCDemoDbContext dbContext) //Ask CLR for Creating an Object from DbContext
        {
            /* dbContext = new MVCDemoDbContext(); */// dependancyInjectionعلشان كدة هستخدم ال  request الحركة دي هتسبب مشاكل لو انا عملت اكتر من   
            _dbContext = dbContext;
        }
        public int AddDepartment(Department department)
        {
            _dbContext.Add(department);
            return _dbContext.SaveChanges();
        }

        public int DeleteDepartment(Department department)
        {
            _dbContext.Remove(department);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartments()
            => _dbContext.Departments.ToList();

        public Department GetDepartmentById(int id)
        {
            //var department = dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            //if (department is null)
            //    department = dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();    
            //return department;

            // we can replace all above in one line by using "Find()" operator that search for the squence locally first if it not found it will get it remotly
            return _dbContext.Departments.Find(id);
        }

        public int UpdateDepartment(Department department)
        {
           _dbContext.Departments.Update(department);
            return _dbContext.SaveChanges();
        }
    }
}
