using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupDetailDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public int Limit { get; set; }
    public ICollection<StudentDetailDto> Students { get; set; }
    public ICollection<ClassSchedulesGroupDto> ClassSchedules { get; set; }
}
