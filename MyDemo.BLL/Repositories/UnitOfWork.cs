using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MVCDemoDbContext _dbContext;

        public IEmployeeRepository EmployeeRepository { get ; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }

        public UnitOfWork(MVCDemoDbContext dbContext) //1-Ask CLR for Creating an Object from Class Implementing IUnitOfWork that contains all Repositories
                                                      //2-Allow dependancy into the Container of servies    
        {
            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            _dbContext = dbContext;
        }

        public int Complete()
            => _dbContext.SaveChanges();

        public void Dispose() 
            => _dbContext.Dispose();
        
    }
}
