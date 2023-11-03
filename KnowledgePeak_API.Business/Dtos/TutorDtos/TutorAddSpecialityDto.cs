using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorAddSpecialityDto
{
    public string UserName { get; set; }
    public int? SpecialityId { get; set; }
}

