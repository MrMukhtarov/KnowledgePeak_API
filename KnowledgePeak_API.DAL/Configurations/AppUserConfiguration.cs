using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(a => a.Name)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(a => a.Surname)
            .IsRequired()
            .HasMaxLength(32);
        builder.Property(a => a.Age)
            .IsRequired();
        builder.Property(a => a.ImageUrl)
            .IsRequired(false);
        builder.Property(a => a.Gender)
            .IsRequired();
    }
}
