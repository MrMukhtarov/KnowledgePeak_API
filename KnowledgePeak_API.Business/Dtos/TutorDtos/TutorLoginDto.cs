using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class TutorLoginDtoValidator : AbstractValidator<TutorLoginDto>
{
    public TutorLoginDtoValidator()
    {
        RuleFor(t => t.UserName)
          .NotNull()
          .WithMessage("Tutor UserName dont be Null")
          .NotEmpty()
          .WithMessage("Tutor UserName dont be Empty")
          .MinimumLength(3)
          .WithMessage("Tutor UserName length must be greather than 3")
          .MaximumLength(45)
          .WithMessage("Tutor UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Tutor Password dont be Null")
           .NotEmpty()
           .WithMessage("Tutor Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Tutor Password length must be greather than 6");
    }
}
