using CfpService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfpService.DataAccess.DbConfiguration
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Application>
    {
        public void Configure(EntityTypeBuilder<Application> builder)
        {
            builder.Property(p => p.Name)
            .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(300);

            builder.Property(p => p.Outline)
                .HasMaxLength(1000);
        }
    }
}
