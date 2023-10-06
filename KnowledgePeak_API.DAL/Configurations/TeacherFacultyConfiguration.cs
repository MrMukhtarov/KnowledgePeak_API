using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class TeacherFacultyConfiguration : IEntityTypeConfiguration<TeacherFaculty>
{
    public void Configure(EntityTypeBuilder<TeacherFaculty> builder)
    {
        builder.HasOne(tf => tf.Teacher)
            .WithMany(tf => tf.TeacherFaculties)
            .HasForeignKey(tf => tf.TeacherId);
        builder.HasOne(tf => tf.Faculty)
            .WithMany(tf => tf.TeacherFaculties)
            .HasForeignKey(tf => tf.FacultyId);
        builder.Ignore(tf => tf.IsDeleted);
    }
}
