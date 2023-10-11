using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class TutorConfiguration : IEntityTypeConfiguration<Tutor>
{
    public void Configure(EntityTypeBuilder<Tutor> builder)
    {
        builder.Property(t => t.Salary)
            .IsRequired();
        builder.Property(t => t.Status)
            .IsRequired();
        builder.Property(t => t.StartDate)
            .HasDefaultValueSql("(DATEADD(hour,4 , GETUTCDATE()))");
        builder.Property(t => t.EndDate)
            .IsRequired(false);
        builder.Property(t => t.IsDeleted)
            .HasDefaultValue(false)
            .IsRequired();
        builder.HasOne(t => t.Speciality)
            .WithMany(t => t.Tutors)
            .HasForeignKey(t => t.SpecialityId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasMany(t => t.Groups)
            .WithOne(t => t.Tutor)
            .HasForeignKey(t => t.TutorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}