using KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;
using KnowledgePeak_API.Business.Dtos.GroupDtos;
using KnowledgePeak_API.Business.Dtos.SpecialityDtos;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorDetailDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsDeleted { get; set; }
    public SpecialityInfoDto Speciality { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public ICollection<GroupSingleDetailDto> Groups { get; set; }
    public ICollection<ClassScheduleTutorDto> ClassSchedules { get; set; }
}
