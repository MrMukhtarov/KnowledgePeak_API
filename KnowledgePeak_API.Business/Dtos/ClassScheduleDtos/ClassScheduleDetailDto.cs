using KnowledgePeak_API.Business.Dtos.ClassTimeDtos;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.LessonDtos;
using KnowledgePeak_API.Business.Dtos.RoomDtos;
using KnowledgePeak_API.Business.Dtos.TeacherDtos;
using KnowledgePeak_API.Business.Dtos.TutorDtos;
using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;

public record ClassScheduleDetailDto
{
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public Days Day { get; set; }
    public ClassTimeDetailItemDto ClassTime { get; set; }
    public GroupSingleDetailDto Group { get; set; }
    public RoomDetailItemDto Room { get; set; }
    public LessonInfoDto Lesson { get; set; }
    public TeacherInfoDto Teacher { get; set; }
    public TutorInfoDto Tutor { get; set; }
}
