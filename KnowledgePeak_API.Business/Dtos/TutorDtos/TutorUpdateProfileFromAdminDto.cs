using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorUpdateProfileFromAdminDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int Age { get; set; }
    public double Salary { get; set; }
    public string Email { get; set; }
    public IFormFile? ImageFile { get; set; }
    public Status Status { get; set; }
    public Gender Gender { get; set; }
    public int? SpecialityId { get; set; }
    public List<int>? GroupIds { get; set; }
}
public class TutorUpdateProfileFromAdminDtoValidator : AbstractValidator<TutorUpdateProfileFromAdminDto>
{
    public TutorUpdateProfileFromAdminDtoValidator()
    {
        RuleFor(t => t.Name)
   .NotNull()
   .WithMessage("Tutor Name dont be Null")
   .NotEmpty()
   .WithMessage("Tutor Name dont be Empty")
   .MinimumLength(2)
   .WithMessage("Tutor Name length must be greather than 2")
   .MaximumLength(25)
   .WithMessage("Tutor Name length must be less than 25");
        RuleFor(t => t.Surname)
           .NotNull()
           .WithMessage("Tutor Surname dont be Null")
           .NotEmpty()
           .WithMessage("Tutor Surname dont be Empty")
           .MinimumLength(2)
           .WithMessage("Tutor Surname length must be greather than 2")
           .MaximumLength(30)
           .WithMessage("Tutor Surname length must be less than 30");
        RuleFor(t => t.ImageFile)
         .SetValidator(new FileValidator());
        RuleFor(t => t.Age)
            .NotNull()
            .WithMessage("Tutor Age dont be Null")
            .NotEmpty()
            .WithMessage("Tutor Age dont be Empty")
            .GreaterThan(18)
            .WithMessage("Tutor Age must be greather than 18");
        RuleFor(t => t.Salary)
             .NotNull()
            .WithMessage("Tutor Salary dont be Null")
            .NotEmpty()
            .WithMessage("Tutor Salary dont be Empty")
            .GreaterThan(300)
            .WithMessage("Tutor Salary must be greather than 300");
        RuleFor(t => t.Gender)
           .Must(ValidateGender)
           .WithMessage("Ivalid gender ");
        RuleFor(t => t.Email)
           .NotNull()
           .WithMessage("Tutor Email dont be Null")
           .NotEmpty()
           .WithMessage("Tutor Email dont be Empty")
            .Must(t =>
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                var result = regex.Match(t);
                return result.Success;
            })
           .WithMessage("Please enter valid email adress");
        RuleFor(t => t.GroupIds)
             .Must(s => IsDistinct(s))
           .WithMessage("Id can not be repeated");
    }
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }
    private bool IsDistinct(IEnumerable<int> ids)
    {
        var encounteredIds = new HashSet<int>();
        if (ids != null)
        {
            foreach (var id in ids)
            {
                if (encounteredIds.Contains(id)) return false;
                encounteredIds.Add(id);
            }
        }

        return true;
    }
}
