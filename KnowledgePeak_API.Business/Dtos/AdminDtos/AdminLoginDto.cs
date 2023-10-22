using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.AdminDtos;

public record AdminLoginDto
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class AdminLoginDtoValidator : AbstractValidator<AdminLoginDto>
{
    public AdminLoginDtoValidator()
    {
        RuleFor(t => t.UserName)
           .NotNull()
           .WithMessage("Admin UserName dont be Null")
           .NotEmpty()
           .WithMessage("Admin UserName dont be Empty")
           .MinimumLength(3)
           .WithMessage("Admin UserName length must be greather than 3")
           .MaximumLength(45)
           .WithMessage("Admin UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Admin Password dont be Null")
           .NotEmpty()
           .WithMessage("Admin Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Admin Password length must be greather than 6");
    }
}
