using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class ClassTimeConfiguration : IEntityTypeConfiguration<ClassTime>
{
    public void Configure(EntityTypeBuilder<ClassTime> builder)
    {
        builder.Property(c => c.StartTime)
            .IsRequired();
        builder.Property(c => c.EndTime)
            .IsRequired();
    }
}
