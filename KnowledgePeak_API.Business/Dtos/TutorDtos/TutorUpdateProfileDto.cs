﻿using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorUpdateProfileDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public IFormFile? ImageFile { get; set; }
    public int Age { get; set; }
    public Gender Gender { get; set; }
    public string Email { get; set; }
}
public class TutorUpdateProfileDtoValidator : AbstractValidator<TutorUpdateProfileDto>
{
    public TutorUpdateProfileDtoValidator()
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
    }
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }

}
