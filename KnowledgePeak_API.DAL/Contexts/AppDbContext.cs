using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.DAL.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityConfiguration).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<University> Universities { get; set; }
    public DbSet<Setting> Settings { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Speciality> Specialities { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonSpeciality> LessonSpecialities { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherFaculty> TeachersFacultys { get; set; }
    public DbSet<TeacherLesson> TeachersLessons { get; set; }
    public DbSet<TeacherSpeciality> TeacherSpecialities { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Tutor> Tutors { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<ClassTime> ClassTimes { get; set; }
    public DbSet<ClassSchedule> ClassSchedules { get; set; }
    public DbSet<Grade> Grades { get; set; }
}
