using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        //5 Signatures of CRUD Methods
        /*
         * GetAll => IEnumerable from its Domain Model
         * Get"DomainModel Name"ById => DomainModel DataType 
         * Add"DomainModel Name" =>  int... "indicated to [Affected rows like in sql engine]"
         * Update"DomainModel Name" => int... "indicated to [Affected rows like in sql engine]"
         * Delete"DomainModel Name => int... "indicated to [Affected rows like in sql engine]"
         */

        IEnumerable<Department> GetAllDepartments();
        Department GetDepartmentById(int id);
        int AddDepartment(Department department);
        int UpdateDepartment(Department department);
        int DeleteDepartment(Department department);
    }
}
