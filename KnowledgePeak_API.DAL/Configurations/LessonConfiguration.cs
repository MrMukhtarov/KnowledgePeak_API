using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class LessonConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.Property(l => l.Name)
            .IsRequired();
        builder.Property(l => l.Description)
            .IsRequired();
        builder.Property(t => t.IsDeleted)
           .HasDefaultValue(false)
           .IsRequired();
        builder.Property(l => l.Duration)
            .IsRequired();
    }
}
