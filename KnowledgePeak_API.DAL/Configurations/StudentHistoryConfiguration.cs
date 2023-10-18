using KnowledgePeak_API.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KnowledgePeak_API.DAL.Configurations;

public class StudentHistoryConfiguration : IEntityTypeConfiguration<StudentHistory>
{
    public void Configure(EntityTypeBuilder<StudentHistory> builder)
    {
        builder.Property(h => h.HistoryDate)
            .IsRequired();
        builder.HasOne(h => h.Student)
            .WithMany(h => h.StudentHistory)
            .HasForeignKey(h => h.Studentid)
            .OnDelete(DeleteBehavior.NoAction);
        builder.HasOne(b => b.Grade)
            .WithOne(b => b.StudentHistory)
            .HasForeignKey<StudentHistory>(b => b.GradeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
