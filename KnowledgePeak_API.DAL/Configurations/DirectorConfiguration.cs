using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class DirectorConfiguration : IEntityTypeConfiguration<Director>
{
    public void Configure(EntityTypeBuilder<Director> builder)
    {
        builder.Property(d => d.Description)
            .IsRequired();
        builder.Property(d => d.Salary)
            .IsRequired();
        builder.Property(d => d.Status)
            .IsRequired();
        builder.Property(d => d.StartDate)
            .HasDefaultValueSql("DATEADD(hour, 4, GETUTCDATE())");
        builder.Property(d => d.EndDate)
            .IsRequired(false);
        builder.HasOne(d => d.University)
            .WithOne(d => d.Director)
            .HasForeignKey<Director>(d => d.UniversityId)
            .IsRequired();
    }
}
