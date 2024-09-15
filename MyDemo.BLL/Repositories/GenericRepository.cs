using Microsoft.EntityFrameworkCore;
using MyDemo.BLL.Interfaces;
using MyDemo.DAL.Data.Contexts;
using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyDemo.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCDemoDbContext _dbContext;
        
        public GenericRepository(MVCDemoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(T entity)
            => _dbContext.Add(entity);
        
        

        public void Delete(T entity)
            => _dbContext.Remove(entity);
           
        

        public IEnumerable<T> GetAll()
        {
            //This is not the best solution to load department [Eager Loading] We should use "Specification Design Pattern"
            //But we will take it in API Coures [So Don't Forget to come back to this code after API and Refactoring it with specification design pattern]
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>)_dbContext.Set<Employee>().Include(E => E.Department).ToList();
            else
                return _dbContext.Set<T>().ToList();
        }
        

        public T GetById(int id)
            => _dbContext.Set<T>().Find(id);

        public void Update(T entity)
            => _dbContext.Update(entity);
          
        
    }
}
