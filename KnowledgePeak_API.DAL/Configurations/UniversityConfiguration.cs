using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class UniversityConfiguration : IEntityTypeConfiguration<University>
{
    public void Configure(EntityTypeBuilder<University> builder)
    {
        builder.Property(u => u.Name)
            .IsRequired();
        builder.Property(u => u.Description)
            .IsRequired();
    }
}
