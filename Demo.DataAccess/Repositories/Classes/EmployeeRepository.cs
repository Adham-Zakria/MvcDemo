using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    
    public class EmployeeRepository(AppDbContext appDbContext)
        : GenericRepository<Employee>(appDbContext), IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext;

       

        //public Employee? GetById(int id)
        //  => _appDbContext.Employees.Find(id);
        
        //public IEnumerable<Employee> GetAll(bool IsTracking = false)
        //{
        //    if (IsTracking)
        //        return _appDbContext.Employees.ToList();
        //    else
        //        return _appDbContext.Employees.AsNoTracking().ToList();
        //}

        //public int Add(Employee employee)
        //{
        //    _appDbContext.Employees.Add(employee);
        //    return _appDbContext.SaveChanges();
        //}

        //public int Update(Employee employee)
        //{
        //    _appDbContext.Employees.Update(employee);
        //    return _appDbContext.SaveChanges();
        //}

        //public int Remove(Employee employee)
        //{
        //    _appDbContext.Employees.Remove(employee);
        //    return _appDbContext.SaveChanges();
        //}
    }
}
