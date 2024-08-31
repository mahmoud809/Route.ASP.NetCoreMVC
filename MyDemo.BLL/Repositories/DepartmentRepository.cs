using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Contexts;
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

        private readonly  MVCDemoDbContext dbContext;
        public DepartmentRepository(MVCDemoDbContext _dbContext) //Ask CLR for Creating an Object from DbContext
        {
            /* dbContext = new MVCDemoDbContext(); */// dependancyInjectionعلشان كدة هستخدم ال  request الحركة دي هتسبب مشاكل لو انا عملت اكتر من   
            dbContext = _dbContext;
        }
        public int AddDepartment(Department department)
        {
            dbContext.Add(department);
            return dbContext.SaveChanges();
        }

        public int DeleteDepartment(Department department)
        {
            dbContext.Remove(department);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Department> GetAllDepartments()
            => dbContext.Departments.ToList();

        public Department GetDepartmentById(int id)
        {
            //var department = dbContext.Departments.Local.Where(D => D.Id == id).FirstOrDefault();
            //if (department is null)
            //    department = dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();    
            //return department;

            // we can replace all above in one line by using "Find()" operator that search for the squence locally first if it not found it will get it remotly
            return dbContext.Departments.Find(id);
        }

        public int UpdateDepartment(Department department)
        {
           dbContext.Departments.Update(department);
            return dbContext.SaveChanges();
        }
    }
}
