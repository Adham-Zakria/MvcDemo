using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDTOs;
using Demo.BusinessLogic.Services.AttachmentService;
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
    public class EmployeeService( /* IEmployeeRepository _employeeRepository */ 
                                 IUnitOfWork _unitOfWork ,
                                 IMapper _mapper ,
                                 IAttachmentService _attachmentService) : IEmployeeService
    {
        public IEnumerable<GetEmployeeDto> GetAllEmployees(string? EmployeeSearchName)
        {
            IEnumerable<Employee> employees;

            if (string.IsNullOrWhiteSpace(EmployeeSearchName))
                employees = _unitOfWork.EmployeeRepository.GetAll();
            else
            {
                employees = _unitOfWork.EmployeeRepository.GetAll(e => e.Name.ToLower()
                                                           .Contains(EmployeeSearchName.ToLower()));
            }
            // mapping from IEnumerable<Employee> to IEnumerable<GetEmployeeDto>
            var employeesDtos = _mapper.Map<IEnumerable<GetEmployeeDto>>( employees );
            return employeesDtos;
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee= _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return null;
            else
                return _mapper.Map<EmployeeDetailsDto>( employee );
            
        }

        public int CreateEmployee(CreateEmployeeDto createEmployeeDto)
        {
            var mappedEmployee=_mapper.Map<Employee>( createEmployeeDto );
            
            // call AttachmentService to upload employee's image
            var imageName = _attachmentService.Upload(createEmployeeDto.Image, "Images");
            mappedEmployee.ImageName = imageName;
            
            /*var res= */ _unitOfWork.EmployeeRepository.Add(mappedEmployee);
            // return res;
            return _unitOfWork.SaveChanges();
        }
        public int UpdateEmployee(UpdateEmployeeDto updateEmployeeDto)
        {
            var mappedEmployee = _mapper.Map<Employee>(updateEmployeeDto);
            /*var res= */ _unitOfWork.EmployeeRepository.Update(mappedEmployee);
            // return res;
            return _unitOfWork.SaveChanges();
        }
        public bool DeleteEmployee(int id)
        {
            var employee= _unitOfWork.EmployeeRepository.GetById(id);
            if (employee is null) return false;
            else
            {
                // Soft delete
                employee.IsDeleted = true;            
                /*var res= */ _unitOfWork.EmployeeRepository.Remove(employee);
                // return res > 0 ? true : false;
                return _unitOfWork.SaveChanges() > 0 ? true : false;
            }
        }
        
    }
}
