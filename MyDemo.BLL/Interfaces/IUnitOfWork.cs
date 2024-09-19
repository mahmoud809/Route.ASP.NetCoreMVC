using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.BLL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        //Signature for Repositories
        public IEmployeeRepository EmployeeRepository { get; set; } // It's just a signature
        public IDepartmentRepository DepartmentRepository { get; set; } // It's just a signature

        public Task<int> Complete();
        
    }
}
