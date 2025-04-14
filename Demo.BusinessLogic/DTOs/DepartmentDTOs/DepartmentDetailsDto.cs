using Demo.DataAccess.Models.DepartmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.DTOs.DepartmentDTOs
{
    public class DepartmentDetailsDto : DepartmentDto
    {
        public DepartmentDetailsDto() { }

        public DepartmentDetailsDto(Department department)
        {
            DeptId = department.Id;
            Name = department.Name;
            Code = department.Code;
            Description = department.Description;
            DateOfCreation = department.CreatedOn;
            CreatedBy = department.CreatedBy;
            LastModifiedBy = department.LastModifiedBy;
            IsDeleted = department.IsDeleted;
            LastModifiedOn = department.LastModifiedOn;
        }   // constructor mapping (Manual mapping)

        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }
        public DateTime? LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
