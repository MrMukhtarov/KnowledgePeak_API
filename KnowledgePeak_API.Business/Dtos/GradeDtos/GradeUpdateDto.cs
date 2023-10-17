using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.GradeDtos;

public record GradeUpdateDto
{
    public int id { get; set; }
    public string StudentId { get; set; }
    public int LessonId { get; set; }
    public double Point { get; set; }
    public string Review { get; set; }
}
public class GradeUpdateDtoValidator : AbstractValidator<GradeUpdateDto>
{
    public GradeUpdateDtoValidator()
    {
        RuleFor(g => g.Point)
         .NotNull()
         .WithMessage("Point not be null")
         .NotEmpty()
         .WithMessage("Point not be empty")
         .GreaterThan(-1)
         .WithMessage("Point must be grather than -1")
           .LessThan(101)
         .WithMessage("Point must be less than 100");
        RuleFor(g => g.Review)
            .NotNull()
            .WithMessage("Review not be null")
            .NotEmpty()
            .WithMessage("Review not be empty");
        RuleFor(g => g.StudentId)
           .NotNull()
           .WithMessage("StudentId not be null")
           .NotEmpty()
           .WithMessage("StudentId not be empty");
        RuleFor(g => g.LessonId)
         .NotNull()
         .WithMessage("LessonId not be null")
         .NotEmpty()
         .WithMessage("LessonId not be empty");
    }
}
