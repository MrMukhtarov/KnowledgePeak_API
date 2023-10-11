using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class SpecialityConfiguration : IEntityTypeConfiguration<Speciality>
{
    public void Configure(EntityTypeBuilder<Speciality> builder)
    {
        builder.Property(s => s.Name)
            .IsRequired();
        builder.Property(s => s.ShortName)
            .IsRequired();
        builder.Property(t => t.IsDeleted)
           .HasDefaultValue(false)
           .IsRequired();
        builder.Property(s => s.CreateTime)
            .HasDefaultValueSql("DATEADD(hour, 4, GETUTCDATE())");
        builder.HasOne(s => s.Faculty)
            .WithMany(s => s.Specialities)
            .HasForeignKey(s => s.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
