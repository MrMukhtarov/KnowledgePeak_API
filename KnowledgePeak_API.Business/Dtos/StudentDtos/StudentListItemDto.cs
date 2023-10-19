using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.StudentHistoryDtos;
using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Business.Dtos.StudentDtos;

public record StudentListItemDto
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string UserName { get; set; }
    public string? ImageUrl { get; set; }
    public Gender Gender { get; set; }
    public double Age { get; set; }
    public string Email { get; set; }
    public int Course { get; set; }
    public Status Status { get; set; }
    public double? Avarage { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public GroupSingleDetailDto Group { get; set; }
    public ICollection<ClassScheduleStudentDto> ClassSchedules { get; set; }
    public ICollection<StudentHistoryInfoDto> StudentHistory { get; set; }
}
