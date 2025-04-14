using Demo.DataAccess.Models.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DataAccess.Data.Configurations
{
    internal class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(dept => dept.CreatedOn).HasDefaultValueSql("GETDATE()");
            // if the row is inserted without value , the default value will be used
            // on insert
            // can't reference other column
            // can be overriden

            builder.Property(dept => dept.LastModifiedOn).HasComputedColumnSql("GETDATE()");
            // value is computed every time the record changed 
            // on update
            // can reference other column


        }
    }
}
