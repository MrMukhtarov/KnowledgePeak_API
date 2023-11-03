using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using KnowledgePeak_API.Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.StudentDtos;

public record StudentUpdateDto
{
    public IFormFile? ImageFile { get; set; }
}
public class StudentUpdateDtoValidator : AbstractValidator<StudentUpdateDto>
{
    public StudentUpdateDtoValidator()
    {
        RuleFor(t => t.ImageFile)
         .SetValidator(new FileValidator());
    }
    private bool ValidateGender(Gender gender)
    {
        return Enum.IsDefined(typeof(Gender), gender);
    }
}
