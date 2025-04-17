using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDTOs;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class EmployeeService(IEmployeeRepository _employeeRepository ,IMapper _mapper ) : IEmployeeService
    {
        public IEnumerable<GetEmployeeDto> GetAllEmployees()
        {
            var employees = _employeeRepository.GetAll();

            // mapping from IEnumerable<Employee> to IEnumerable<GetEmployeeDto>
            var employeesDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>( employees );
            return employeesDtos;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee=_employeeRepository.GetById(id);
            if (employee is null) return null;
            else
                return _mapper.Map<EmployeeDetailsDto>( employee );
            
        }

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var mappedEmployee=_mapper.Map<Employee>( createEmployeeDto );
            var res=_employeeRepository.Add(mappedEmployee);
            return res;
        }
        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
            var res = _employeeRepository.Update(mappedEmployee);
            return res;
        }
        public bool DeleteEmployee(int id)
        {
            var employee=_employeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                // Soft delete
                employee.IsDeleted = true;            
                var res= _employeeRepository.Remove(employee);
                return res > 0 ? true : false;
            }
        }
        
    }
}
