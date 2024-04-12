using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Models;

namespace CfpService.DataAccess.DbConfiguration
{
    public class ApplicationConfiguration : IEntityTypeConfiguration<Domain.Models.Application>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Application> builder)
        {
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name)
            .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(300);

            builder.Property(p => p.Outline)
                .HasMaxLength(1000);
        }
    }
}
