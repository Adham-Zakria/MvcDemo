using Demo.BusinessLogic.DTOs.DepartmentDTOs;
using Demo.BusinessLogic.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Interfaces
{
    public interface IEmployeeService
    {
        IEnumerable<GetEmployeeDto> GetAllEmployees();

        int CreateEmployee(CreateEmployeeDto createEmployeeDto);
        EmployeeDetailsDto? GetEmployeeById(int id);
        int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto);
        public bool DeleteEmployee(int id);
    }
}
