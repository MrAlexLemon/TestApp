using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProcessComputerInfoFunction.Persistence
{
    public class ComputerInfoConfiguration : IEntityTypeConfiguration<ComputerInfo>
    {
        public void Configure(EntityTypeBuilder<ComputerInfo> builder)
        {
            builder.HasKey(t => t.ComputerName);
        }
    }
}
