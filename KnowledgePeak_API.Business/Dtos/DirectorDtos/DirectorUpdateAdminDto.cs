using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.DirectorDtos;

public record DirectorUpdateAdminDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Status Status { get; set; }
    public decimal Salary { get; set; }
}
public class DirectorUpdateAdminDtoValidator : AbstractValidator<DirectorUpdateAdminDto>
{
    public DirectorUpdateAdminDtoValidator()
    {
        RuleFor(t => t.Name)
           .NotNull()
           .WithMessage("Director Name dont be Null")
           .NotEmpty()
           .WithMessage("Director Name dont be Empty")
           .MinimumLength(2)
           .WithMessage("Director Name length must be greather than 2")
           .MaximumLength(25)
           .WithMessage("Director Name length must be less than 25");
        RuleFor(t => t.Surname)
           .NotNull()
           .WithMessage("Director Surname dont be Null")
           .NotEmpty()
           .WithMessage("Director Surname dont be Empty")
           .MinimumLength(2)
           .WithMessage("Director Surname length must be greather than 2")
           .MaximumLength(30)
           .WithMessage("Director Surname length must be less than 30");
        RuleFor(t => t.Description)
           .NotNull()
           .WithMessage("Director Description dont be Null")
           .NotEmpty()
           .WithMessage("Director Description dont be Empty")
           .MinimumLength(2)
           .WithMessage("Director Description length must be greather than 2");
        RuleFor(t => t.ImageFile)
           .SetValidator(new FileValidator());
        RuleFor(t => t.Age)
            .NotNull()
            .WithMessage("Director Age dont be Null")
            .NotEmpty()
            .WithMessage("Director Age dont be Empty")
            .GreaterThan(18)
            .WithMessage("Director Age must be greather than 18");
        RuleFor(t => t.Salary)
            .NotNull()
            .WithMessage("Director Salary dont be Null")
            .NotEmpty()
            .WithMessage("Director Salary dont be Empty")
            .GreaterThan(350)
            .WithMessage("Director Salary must be greather than 350");
        RuleFor(t => t.Gender)
            .Must(ValidateGender)
            .WithMessage("Ivalid gender ");
        RuleFor(t => t.Email)
           .NotNull()
           .WithMessage("Director Email dont be Null")
           .NotEmpty()
           .WithMessage("Director Email dont be Empty")
            .Must(t =>
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var result = regex.Match(t);
                return result.Success;
            })
           .WithMessage("Please enter valid email adress");
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
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }
}
