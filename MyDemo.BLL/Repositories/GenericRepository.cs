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
        public async Task Add(T entity)
            => await _dbContext.AddAsync(entity);
        
        

        public void Delete(T entity)
            => _dbContext.Remove(entity);
           
        

        public async Task<IEnumerable<T>> GetAll()
        {
            //This is not the best solution to load department [Eager Loading] We should use "Specification Design Pattern"
            //But we will take it in API Coures [So Don't Forget to come back to this code after API and Refactoring it with specification design pattern]
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>) await _dbContext.Set<Employee>().Include(E => E.Department).ToListAsync();
            else
                return await _dbContext.Set<T>().ToListAsync();
        }
        

        public async Task<T> GetById(int id)
            => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T entity)
            => _dbContext.Update(entity);
          
        
    }
}
