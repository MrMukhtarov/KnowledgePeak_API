using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherCreateDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }
    public double Salary { get; set; }
    public IFormFile? ImageFile { get; set; }
    public Gender Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
public class TeacherCreateDtoValidator : AbstractValidator<TeacherCreateDto>
{
    public TeacherCreateDtoValidator()
    {
        RuleFor(t => t.Name)
         .NotNull()
         .WithMessage("Teacher Name dont be Null")
         .NotEmpty()
         .WithMessage("Teacher Name dont be Empty")
         .MinimumLength(2)
         .WithMessage("Teacher Name length must be greather than 2")
         .MaximumLength(25)
         .WithMessage("Teacher Name length must be less than 25");
        RuleFor(t => t.Surname)
           .NotNull()
           .WithMessage("Teacher Surname dont be Null")
           .NotEmpty()
           .WithMessage("Teacher Surname dont be Empty")
           .MinimumLength(2)
           .WithMessage("Teacher Surname length must be greather than 2")
           .MaximumLength(30)
           .WithMessage("Teacher Surname length must be less than 30");
        RuleFor(t => t.Description)
           .NotNull()
           .WithMessage("Teacher Description dont be Null")
           .NotEmpty()
           .WithMessage("Teacher Description dont be Empty")
           .MinimumLength(2)
           .WithMessage("Teacher Description length must be greather than 2");
        RuleFor(t => t.Age)
           .NotNull()
           .WithMessage("Teacher Age dont be Null")
           .NotEmpty()
           .WithMessage("Teacher Age dont be Empty")
           .GreaterThan(18)
           .WithMessage("Teacher Age must be greather than 18");
        RuleFor(t => t.Salary)
           .NotNull()
           .WithMessage("Teacher Salary dont be Null")
           .NotEmpty()
           .WithMessage("Teacher Salary dont be Empty")
           .GreaterThan(350)
           .WithMessage("Teacher Salary must be greather than 350");
        RuleFor(t => t.ImageFile)
            .SetValidator(new FileValidator());
        RuleFor(t => t.Gender)
           .Must(ValidateGender)
           .WithMessage("Ivalid gender ");
        RuleFor(t => t.Email)
          .NotNull()
          .WithMessage("Teacher Email dont be Null")
          .NotEmpty()
          .WithMessage("Teacher Email dont be Empty")
           .Must(t =>
           {
               Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
               var result = regex.Match(t);
               return result.Success;
           })
          .WithMessage("Please enter valid email adress");
        RuleFor(t => t.UserName)
           .NotNull()
           .WithMessage("Teacher UserName dont be Null")
           .NotEmpty()
           .WithMessage("Teacher UserName dont be Empty")
           .MinimumLength(3)
           .WithMessage("Teacher UserName length must be greather than 3")
           .MaximumLength(45)
           .WithMessage("Teacher UserName length must be less than 45");
        RuleFor(t => t.Password)
           .NotNull()
           .WithMessage("Teacher Password dont be Null")
           .NotEmpty()
           .WithMessage("Teacher Password dont be Empty")
           .MinimumLength(6)
           .WithMessage("Teacher Password length must be greather than 6");
    }
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }
}
