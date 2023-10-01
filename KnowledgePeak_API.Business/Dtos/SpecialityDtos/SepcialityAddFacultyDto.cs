using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SepcialityAddFacultyDto
{
    public int FacultyId { get; set; }
}
public class SepcialityAddFacultyDtoValidator : AbstractValidator<SepcialityAddFacultyDto>
{
    public SepcialityAddFacultyDtoValidator()
    {
        RuleFor(s => s.FacultyId)
           .GreaterThan(0)
           .WithMessage("FacultyId graether than 0")
           .NotEmpty()
           .WithMessage("FacultyId not be null");
    }
}
