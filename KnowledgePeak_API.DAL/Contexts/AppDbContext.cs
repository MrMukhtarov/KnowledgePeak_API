using Microsoft.EntityFrameworkCore;

namespace KnowledgePeak_API.DAL.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
