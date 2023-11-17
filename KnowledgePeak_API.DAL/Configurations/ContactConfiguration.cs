using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(c => c.FullName)
            .IsRequired();
        builder.Property(c => c.Phone)
            .IsRequired();
        builder.Property(c => c.Email)
            .IsRequired();
        builder.Property(c => c.Message)
            .IsRequired();
        builder.Property(c => c.CreateDate)
            .HasDefaultValueSql("DATEADD(hour,4,GETUTCDATE())");
    }
}
