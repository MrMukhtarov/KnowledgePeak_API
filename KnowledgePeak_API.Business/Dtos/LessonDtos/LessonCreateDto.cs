using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.LessonDtos;

public record LessonCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
}
public class LessonCreateDtoValidator : AbstractValidator<LessonCreateDto>
{
    public LessonCreateDtoValidator()
    {
        RuleFor(l => l.Name)
            .NotNull()
            .WithMessage("Lesson Name not be Null")
            .NotEmpty()
            .WithMessage("Lesson Name not be Empty")
            .MinimumLength(2)
            .WithMessage("Lesson name Length must be grather than 2");
        RuleFor(l => l.Description)
            .NotNull()
            .WithMessage("Lesson Description not be Null")
            .NotEmpty()
            .WithMessage("Lesson Description not be Empty")
            .MinimumLength(2)
            .WithMessage("Lesson Description Length must be grather than 2");
        RuleFor(l => l.Duration)
            .NotEmpty()
            .WithMessage("Lesson Duration not be Empty")
            .NotNull()
            .WithMessage("Lesson Duration not be Null")
            .GreaterThan(0)
            .WithMessage("Lesson DUration must be grather than 0");
    }
}
