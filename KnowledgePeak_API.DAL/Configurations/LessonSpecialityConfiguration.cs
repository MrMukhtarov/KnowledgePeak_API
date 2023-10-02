using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class LessonSpecialityConfiguration : IEntityTypeConfiguration<LessonSpeciality>
{
    public void Configure(EntityTypeBuilder<LessonSpeciality> builder)
    {
        builder.HasOne(ls => ls.Speciality)
            .WithMany(ls => ls.LessonSpecialities)
            .HasForeignKey(ls => ls.SpecialityId);
        builder.HasOne(ls => ls.Lesson)
            .WithMany(ls => ls.LessonSpecialities)
            .HasForeignKey(ls => ls.LessonId);
        builder.Ignore(ls => ls.IsDeleted);
    }
}
