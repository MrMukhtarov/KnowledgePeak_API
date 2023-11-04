using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SepcialityAddFacultyDto
{
    public int? FacultyId { get; set; }
}
