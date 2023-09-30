using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.UniversityDtos;

public class UniversityCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
}
public class UniversityCreateDtoValidator : AbstractValidator<UniversityCreateDto>
{
    public UniversityCreateDtoValidator()
    {
        RuleFor(u => u.Name)
           .NotNull()
           .WithMessage("University name not be null")
           .NotEmpty()
           .WithMessage("University name not be empty")
           .MinimumLength(3)
           .WithMessage("University name length must be greather than 3");
        RuleFor(u => u.Description)
            .NotNull()
            .WithMessage("University Description not be null")
            .NotEmpty()
            .WithMessage("University Description not be empty")
            .MinimumLength(3)
            .WithMessage("University Description length must be greather than 3");
    }
}
