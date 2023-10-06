using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class TeacherLoginDtoValidator : AbstractValidator<TeacherLoginDto>
{
    public TeacherLoginDtoValidator()
    {
        RuleFor(t => t.UserName)
          .NotNull()
          .WithMessage("Director UserName dont be Null")
          .NotEmpty()
          .WithMessage("Director UserName dont be Empty")
          .MinimumLength(3)
          .WithMessage("Director UserName length must be greather than 3")
          .MaximumLength(45)
          .WithMessage("Director UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Director Password dont be Null")
           .NotEmpty()
           .WithMessage("Director Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Director Password length must be greather than 6");
    }
}
