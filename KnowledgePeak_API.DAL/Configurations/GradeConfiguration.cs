using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.Property(g => g.GradeDate)
            .IsRequired()
            .HasDefaultValueSql("DATEADD(hour,4,GETUTCDATE())");
        builder.Property(g => g.Point)
            .IsRequired();
        builder.Property(g => g.Review)
            .IsRequired();
        builder.HasOne(g => g.Teacher)
            .WithMany(g => g.Grades)
            .HasForeignKey(g => g.TeacherId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(g => g.Student)
            .WithMany(g => g.Grades)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(g => g.Lesson)
           .WithMany(g => g.Grades)
           .HasForeignKey(g => g.LessonId)
           .OnDelete(DeleteBehavior.NoAction);
    }
}
