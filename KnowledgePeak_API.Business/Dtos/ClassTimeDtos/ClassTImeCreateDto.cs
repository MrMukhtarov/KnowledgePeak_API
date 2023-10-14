using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.ClassTimeDtos;

public record ClassTImeCreateDto
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}
public class ClassTImeCreateDtoValidator : AbstractValidator<ClassTImeCreateDto>
{
    public ClassTImeCreateDtoValidator()
    {
        RuleFor(c => c.StartTime)
            .NotNull()
            .WithMessage("CLassTime StartTime not be null")
            .NotEmpty()
            .WithMessage("ClassTime StartTime not be empty");
        RuleFor(c => c.EndTime)
          .NotNull()
          .WithMessage("CLassTime EndTime not be null")
          .NotEmpty()
          .WithMessage("ClassTime EndTime not be empty");
    }
}
