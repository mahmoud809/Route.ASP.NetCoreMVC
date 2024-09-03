using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyDemo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDemo.DAL.Data.Configurations
{
    public class DepartmentConfigurations : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(D => D.Name).IsRequired()
                   .HasMaxLength(50);
            
            builder.Property(D => D.Id)
                    .UseIdentityColumn(10, 10);

            builder.Property(D => D.Code).HasMaxLength(50);
        }
    }
}
