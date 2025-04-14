using Demo.DataAccess.Contexts;
using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    // CRUD operations : get all , get by id , add , edit(update) and delete
    public class DepartmentRepository(AppDbContext appDbContext) /* primary constructor */ 
        : GenericRepository<Department>(appDbContext) /* constructor chaining */ , IDepartmentRepository
    
    {
        private readonly AppDbContext _appDbContext = appDbContext;

        //public DepartmentRepository(AppDbContext appDbContext) // dependency injection
        //{
        //    _appDbContext = appDbContext;
        //}

        //public Department? GetById(int id)
        //  => _appDbContext.Departments.Find(id);
        ////{
        ////    var department=_appDbContext.Departments.Find(id);
        ////    return department;
        ////}

        //public IEnumerable<Department> GetAll(bool IsTracking = false)
        //{
        //    if (IsTracking)
        //        return _appDbContext.Departments.ToList();
        //    else
        //        return _appDbContext.Departments.AsNoTracking().ToList();
        //}

        //public int Add(Department dept)
        //{
        //    _appDbContext.Departments.Add(dept);
        //    return _appDbContext.SaveChanges();
        //}

        //public int Update(Department dept)
        //{
        //    _appDbContext.Departments.Update(dept);
        //    return _appDbContext.SaveChanges();
        //}

        //public int Remove(Department dept)
        //{
        //    _appDbContext.Departments.Remove(dept);
        //    return _appDbContext.SaveChanges();
        //}
    }
}
