using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class ClassScheduleCOnfiguration : IEntityTypeConfiguration<ClassSchedule>
{
    public void Configure(EntityTypeBuilder<ClassSchedule> builder)
    {
        builder.HasOne(l => l.Lesson)
            .WithMany(l => l.ClassSchedules)
            .HasForeignKey(l => l.LessonId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.ClassTime)
            .WithMany(l => l.ClassSchedules)
            .HasForeignKey(l => l.ClassTimeId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.Tutor)
            .WithMany(l => l.ClassSchedules)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(l => l.Group)
            .WithMany(l => l.ClassSchedules)
            .HasForeignKey(l => l.GroupId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.Room)
            .WithMany(c => c.ClassSchedules)
            .HasForeignKey(c => c.RoomId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(c => c.Teacher)
            .WithMany(c => c.ClassSchedules)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.NoAction);
        builder.Property(l => l.ScheduleDate)
            .IsRequired();
    }
}
