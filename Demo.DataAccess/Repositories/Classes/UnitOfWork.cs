using Demo.DataAccess.Contexts;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Repositories.Classes
{
    public class UnitOfWork : IUnitOfWork   /* IDisposable */
    {
        //private readonly IEmployeeRepository _employeeRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;

        private readonly AppDbContext _appDbContext;

        public UnitOfWork(/* IEmployeeRepository employeeRepository , 
                          IDepartmentRepository departmentRepository , */
                          AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            //_employeeRepository = employeeRepository;
            //_departmentRepository = departmentRepository;
            _employeeRepository= new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_appDbContext));
            _departmentRepository= new Lazy<IDepartmentRepository>(() => new DepartmentRepository(_appDbContext));
        }

        //public IEmployeeRepository EmployeeRepository => _employeeRepository;
        public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;

        //public IDepartmentRepository DepartmentRepository => _departmentRepository;
        public IDepartmentRepository DepartmentRepository => _departmentRepository.Value;

        public int SaveChanges()
        {
            return _appDbContext.SaveChanges();
        }

        /*public void Dispose()
        {
            throw new NotImplementedException();
        }*/
    }
}
