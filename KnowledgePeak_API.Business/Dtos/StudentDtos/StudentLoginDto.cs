using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.StudentDtos;

public record StudentLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class StudentLoginDtoValidator : AbstractValidator<StudentLoginDto>
{
    public StudentLoginDtoValidator()
    {
        RuleFor(t => t.UserName)
          .NotNull()
          .WithMessage("Student UserName dont be Null")
          .NotEmpty()
          .WithMessage("Student UserName dont be Empty")
          .MinimumLength(3)
          .WithMessage("Student UserName length must be greather than 3")
          .MaximumLength(45)
          .WithMessage("Student UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Student Password dont be Null")
           .NotEmpty()
           .WithMessage("Student Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Student Password length must be greather than 6");
    }
}
