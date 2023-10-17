using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Contexts;
using KnowledgePeak_API.DAL.Repositories.Interfaces;

namespace KnowledgePeak_API.DAL.Repositories.Implements;

public class GradeRepository : Repository<Grade>, IGradeRepository
{
    public GradeRepository(AppDbContext context) : base(context)
    {
    }
}
