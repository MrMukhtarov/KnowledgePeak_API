using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.Property(t => t.Description)
            .IsRequired();
        builder.Property(t => t.Salary)
            .IsRequired();
        builder.Property(t => t.Status)
            .IsRequired();
        builder.Property(t => t.IsDeleted)
           .HasDefaultValue(false)
           .IsRequired();
        builder.Property(t => t.StartDate)
            .HasDefaultValueSql("DATEADD(hour, 4, GETUTCDATE())");
        builder.Property(t => t.EndDate)
            .IsRequired(false);
    }
}
