using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class TeacherLessonConfiguration : IEntityTypeConfiguration<TeacherLesson>
{
    public void Configure(EntityTypeBuilder<TeacherLesson> builder)
    {
        builder.HasOne(ts => ts.Teacher)
            .WithMany(ts => ts.TeacherLessons)
            .HasForeignKey(ts => ts.TeacherId);
        builder.HasOne(ts => ts.Lesson)
            .WithMany(ts => ts.TeacherLessons)
            .HasForeignKey(ts => ts.LessonId);
        builder.Ignore(ts => ts.IsDeleted);
    }
}
