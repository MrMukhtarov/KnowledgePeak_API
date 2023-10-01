using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityUpdateDto
{
    public string Name { get; set; }
    public string ShortName { get; set; }
    public int? FacultyId { get; set; }
}
public class SpecialityUpdateDtoValidator : AbstractValidator<SpecialityUpdateDto>
{
    public SpecialityUpdateDtoValidator()
    {
        RuleFor(s => s.Name)
          .NotNull()
          .WithMessage("Speciality name not be null")
          .NotEmpty()
          .WithMessage("Speciality name not be empty")
          .MinimumLength(2)
          .WithMessage("Speciality name length must be greather than 2");
        RuleFor(s => s.ShortName)
            .NotNull()
            .WithMessage("Speciality ShortName not be null")
            .NotEmpty()
            .WithMessage("Speciality ShortName not be empty")
            .MinimumLength(2)
            .WithMessage("Speciality ShortName length must be greather than 2");
        RuleFor(s => s.FacultyId)
           .GreaterThan(0)
           .WithMessage("FacultyId graether than 0")
           .NotEmpty()
           .WithMessage("FacultyId not be null");
    }
}
