using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebService.Entities;

namespace WebService.Persistence.Configurations
{
    public class ComputerInfoConfiguration : IEntityTypeConfiguration<ComputerInfo>
    {
        public void Configure(EntityTypeBuilder<ComputerInfo> builder)
        {
            builder.HasKey(t => t.ComputerName);

            builder.Property(t => t.OsName)
                .IsRequired();

            builder.Property(t => t.DotNetVersion)
                .IsRequired();

            builder.Property(t => t.TimeZone)
                .IsRequired();

            builder.Property(t => t.ConnectedTime)
                .IsRequired();
        }
    }
}
