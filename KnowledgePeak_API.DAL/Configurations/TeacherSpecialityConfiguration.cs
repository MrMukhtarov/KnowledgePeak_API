using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class TeacherSpecialityConfiguration : IEntityTypeConfiguration<TeacherSpeciality>
{
    public void Configure(EntityTypeBuilder<TeacherSpeciality> builder)
    {
        builder.HasOne(ts => ts.Teacher)
            .WithMany(ts => ts.TeacherSpecialities)
            .HasForeignKey(ts => ts.TeacherId);
        builder.HasOne(ts => ts.Speciality)
            .WithMany(ts => ts.TeacherSpecialities)
            .HasForeignKey(ts => ts.SpecialityId);
        builder.Ignore(ts => ts.IsDeleted);
    }
}
