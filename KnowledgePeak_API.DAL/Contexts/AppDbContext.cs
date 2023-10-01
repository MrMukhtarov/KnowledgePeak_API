using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.DAL.Contexts;

public class AppDbContext : DbContext
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
}
