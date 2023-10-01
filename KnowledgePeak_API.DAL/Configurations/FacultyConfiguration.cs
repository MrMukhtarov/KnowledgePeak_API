using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.Property(f => f.Name)
            .IsRequired();
        builder.Property(f => f.ShortName)
            .IsRequired();
        builder.Property(f => f.CreateTime)
            .HasDefaultValueSql("getutcdate()");
    }
}
