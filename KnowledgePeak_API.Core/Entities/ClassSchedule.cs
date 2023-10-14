using KnowledgePeak_API.Core.Entities.Commons;
using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Core.Entities;

public class ClassSchedule : BaseEntity
{
    public Lesson Lesson { get; set; }
    public int LessonId { get; set; }
    public Days Day { get; set; }
    public ClassTime ClassTime { get; set; }
    public int ClassTimeId { get; set; } 
    public Tutor Tutor { get; set; }
    public string TutorId { get; set; }
    public Group Group { get; set; }
    public int GroupId { get; set; }
    public Room Room { get; set; }
    public int RoomId { get; set; }
    public DateTime ScheduleDate { get; set; }
    public Teacher Teacher { get; set; }
    public string TeacherId { get; set; }
}
