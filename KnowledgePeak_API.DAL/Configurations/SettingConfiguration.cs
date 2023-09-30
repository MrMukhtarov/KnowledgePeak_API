using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class SettingConfiguration : IEntityTypeConfiguration<Setting>
{
    public void Configure(EntityTypeBuilder<Setting> builder)
    {
        builder.Property(s => s.Email)
            .IsRequired()
            .HasAnnotation("RegularExpression", "[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}");
        builder.Property(s => s.Phone)
            .IsRequired()
            .HasAnnotation("RegularExpression", @"^(\+994|0)(50|51|55|70|77|99)[1-9]\d{6}$");
        builder.Property(s => s.Location)
            .IsRequired();
        builder.Property(s => s.HeaderLogo)
            .IsRequired();
        builder.Property(s => s.FooterLogo)
            .IsRequired();
        builder.Ignore(s => s.IsDeleted);
    }
}
