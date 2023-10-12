using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasIndex(r => r.RoomNumber)
            .IsUnique();
        builder.Property(r => r.Capacity)
            .IsRequired();
        builder.Property(r => r.IsEmpty)
            .HasDefaultValue(true);
        builder.HasOne(r => r.Faculty)
            .WithMany(r => r.Rooms)
            .HasForeignKey(r => r.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
