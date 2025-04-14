using Demo.BusinessLogic.DTOs.DepartmentDTOs;
using Demo.BusinessLogic.Factories;
using Demo.BusinessLogic.Services.Interfaces;
using Demo.DataAccess.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Services.Classes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentDto> GetAllDepartments()
        {
            var departments = _departmentRepository.GetAll();
            //var departmentToReturn = departments.Select(d => new DepartmentDto ()
            //{
            //    DeptId = d.Id,
            //    Name = d.Name,
            //    Code = d.Code,
            //    Description = d.Description,
            //    DateOfCreation =d.CreatedOn
            //});   // Manual mapping
            var departmentToReturn = departments.Select(d => d.ToDepartmentDto());
            return departmentToReturn;
        }

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepository.GetById(id);

            //if(departmet is null)
            //    return null;
            //else 
            //{
            //    var departmentToReturn= new DepartmentDetailsDto() 
            //    {
            //        DeptId = departmet.Id,
            //        Name = departmet.Name,
            //        Code = departmet.Code,
            //        Description = departmet.Description,
            //        DateOfCreation=departmet.CreatedOn ,
            //        IsDeleted = departmet.IsDeleted ,
            //        LastModifiedBy = departmet.LastModifiedBy,
            //        CreatedBy = departmet.CreatedBy

            //    };
            //    return departmentToReturn;
            //}
            return department == null ? null : new DepartmentDetailsDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn,
                IsDeleted = department.IsDeleted,
                LastModifiedBy = department.LastModifiedBy,
                CreatedBy = department.CreatedBy

            };

            // return department == null ? null : department.ToDepartmentDetailsDto();

        }


        public int CreateDepartment(CreateDepartmentDto createDepartmentDto)
        {
            var res = _departmentRepository.Add(createDepartmentDto.ToEntity()); // convert CreateDepartmentDto (dto) to department (model)
            return res;
        }

        public int UpdateDepartment(UpdateDepartmentDto updateDepartmentDto)
        {
            var res = _departmentRepository.Update(updateDepartmentDto.ToEntity());
            return res;
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.GetById(id);
            if (department is null) return false;
            else
            {
                int res = _departmentRepository.Remove(department);
                return res > 0 ? true : false;
            }
        }
    }
}
