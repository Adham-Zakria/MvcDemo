using Demo.DataAccess.Models.DepartmentModels;
using Demo.DataAccess.Models.EmployeeModels;
using Demo.DataAccess.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    internal class EmployeeConfiguration : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e=>e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Email).HasColumnType("varchar(30)");
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(11)");

            builder.Property(e => e.Gender).HasConversion(
                ValueToAddInDb => ValueToAddInDb.ToString() ,    // convert to string when adding to database
                ValueToReadFromDb=> (Gender) Enum.Parse(typeof(Gender), ValueToReadFromDb)) // convert to enum again when reading from database
                .HasColumnType("varchar(6)"); 

            builder.Property(e => e.EmployeeType).HasConversion(
                ValueToAddInDb => ValueToAddInDb.ToString(),
                ValueToReadFromDb => (EmployeeType)Enum.Parse(typeof(EmployeeType), ValueToReadFromDb))
                .HasColumnType("varchar(8)");

            base.Configure(builder);

        }
    }
}
