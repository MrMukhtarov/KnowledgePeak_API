using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityCreateDto
{
    public string Name { get; set; }
    public string ShortName { get; set; }
}
public class SpecialityCreateDtoValidator : AbstractValidator<SpecialityCreateDto>
{
    public SpecialityCreateDtoValidator()
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
    }
}