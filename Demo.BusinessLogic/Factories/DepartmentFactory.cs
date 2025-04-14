using Demo.BusinessLogic.DTOs.DepartmentDTOs;
using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Factories
{
    public static class DepartmentFactory  // extention method mapping (Manual)
    {
        //Department.ToDepartmentDto();
        public static DepartmentDto ToDepartmentDto(this Department department)
        {
            return new DepartmentDto()
            {
                DeptId = department.Id,
                Name = department.Name,
                Code = department.Code,
                Description = department.Description,
                DateOfCreation = department.CreatedOn
            };

        }

        public static DepartmentDetailsDto ToDepartmentDetailsDto(this Department department)
        {
            return new DepartmentDetailsDto()
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

        }

        public static Department ToEntity(this CreateDepartmentDto Dto) 
        {
            return new Department()
            {
                Name = Dto.Name,
                Code = Dto.Code,
                Description = Dto.Description,
                CreatedOn = Dto.DateOfCreation
            };
        }

        public static Department ToEntity(this UpdateDepartmentDto Dto)
        {
            return new Department()
            {
                Id = Dto.Id,
                Name = Dto.Name,
                Code = Dto.Code,
                Description = Dto.Description,
                CreatedOn = Dto.DateOfCreation
            };
        }
    }
}
