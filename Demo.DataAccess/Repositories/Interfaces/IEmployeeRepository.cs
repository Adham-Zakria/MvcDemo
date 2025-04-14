using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Interfaces
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        //int Add(Employee employee);
        //IEnumerable<Employee> GetAll(bool IsTracking = false);
        //Employee? GetById(int id);
        //int Remove(Employee employee);
        //int Update(Employee employee);
    }
}
