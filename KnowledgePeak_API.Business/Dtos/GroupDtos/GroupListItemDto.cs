using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;
using KnowledgePeak_API.Business.Dtos.StudentDtos;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupListItemDto
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public string Name { get; set; }
    public int Limit { get; set; }
    public SpecialityInfoDto Speciality { get; set; }
    public ICollection<StudentDetailDto> Students { get; set; }
    public ICollection<ClassSchedulesGroupDto> ClassSchedules { get; set; }
}
