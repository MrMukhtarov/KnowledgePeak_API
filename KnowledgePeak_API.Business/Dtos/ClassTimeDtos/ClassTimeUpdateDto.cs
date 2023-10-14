using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.ClassTimeDtos;

public record ClassTimeUpdateDto
{
    public string StartTime { get; set; }
    public string EndTime { get; set; }
}
public class ClassTimeUpdateDtoValidator : AbstractValidator<ClassTimeUpdateDto>
{
    public ClassTimeUpdateDtoValidator()
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
