using KnowledgePeak_API.Core.Entities;
using KnowledgePeak_API.DAL.Contexts;
using KnowledgePeak_API.DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgePeak_API.DAL.Repositories.Implements;

public class ClassScheduleRepository : Repository<ClassSchedule>, IClassScheduleRepository
{
    public ClassScheduleRepository(AppDbContext context) : base(context)
    {
    }
}
