using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Property(s => s.Status)
            .IsRequired();
        builder.Property(s => s.StartDate)
            .IsRequired(false);
        builder.Property(s => s.EndDate)
            .IsRequired(false);
        builder.Property(t => t.IsDeleted)
           .HasDefaultValue(false)
           .IsRequired();
        builder.HasOne(s => s.Group)
            .WithMany(s => s.Students)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
