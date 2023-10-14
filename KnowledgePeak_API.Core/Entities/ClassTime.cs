using KnowledgePeak_API.Core.Entities.Commons;

namespace KnowledgePeak_API.Core.Entities;

public class ClassTime : BaseEntity
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public ICollection<ClassSchedule> ClassSchedules { get; set; }
}
