using AutoMapper;
using Demo.BusinessLogic.DTOs.EmployeeDTOs;
using Demo.DataAccess.Models.EmployeeModels;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BusinessLogic.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //CreateMap<TSource, TDestination>();
            CreateMap<Employee, GetEmployeeDto>()
                .ForMember(dest => dest.EmpType, sour => sour.MapFrom(emp => emp.EmployeeType))
                .ForMember(dest => dest.EmpGender, sour => sour.MapFrom(emp => emp.Gender)) // different names
                .ForMember(dest => dest.Department, sour => sour.MapFrom(emp => emp.Department != null ? emp.Department.Name : null ));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.EmployeeType, sour => sour.MapFrom(emp => emp.EmployeeType))
                .ForMember(dest => dest.Gender, sour => sour.MapFrom(emp => emp.Gender))
                .ForMember(dest => dest.HiringDate, sour => sour.MapFrom(emp=>DateOnly.FromDateTime(emp.HiringDate)))  // different data types
                .ForMember(dest => dest.Department, sour => sour.MapFrom(emp => emp.Department != null ? emp.Department.Name : null));

            CreateMap<CreateEmployeeDto,Employee>()
                .ForMember(dest=>dest.HiringDate , sour=>sour.MapFrom(emp=>emp.HiringDate.ToDateTime(TimeOnly.MinValue)));  

            CreateMap<UpdateEmployeeDto,Employee>()
                .ForMember(dest => dest.HiringDate, sour => sour.MapFrom(emp => emp.HiringDate.ToDateTime(TimeOnly.MinValue)));

        }
    }
}
