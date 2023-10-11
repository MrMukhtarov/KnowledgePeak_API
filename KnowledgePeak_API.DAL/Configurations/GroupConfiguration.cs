using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(g => g.Name)
            .IsRequired();
        builder.Property(g => g.Limit)
            .IsRequired();
        builder.Property(t => t.IsDeleted)
           .HasDefaultValue(false)
           .IsRequired();
        builder.HasOne(g => g.Speciality)
            .WithMany(g => g.Groups)
            .HasForeignKey(g => g.SpecialityId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.NoAction);
        //builder.HasOne(g => g.Tutor)
        //    .WithMany(g => g.Groups)
        //    .HasForeignKey(g => g.TutorId)
        //    .OnDelete(DeleteBehavior.NoAction);
    }
}
