using FluentValidation;
using KnowledgePeak_API.Business.Validators;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.SettingDtos;

public record SettingUpdateDto
{
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Location { get; set; }
    public IFormFile? HeaderLogoFile { get; set; }
    public IFormFile? FooterLogoFile { get; set; }
}
public class SettingUpdateDtoValidator : AbstractValidator<SettingUpdateDto>
{
    public SettingUpdateDtoValidator()
    {
        RuleFor(s => s.Email)
            .NotNull()
            .WithMessage("Email not be null")
            .NotEmpty()
            .WithMessage("Email not be empty")
            .Must(s =>
             {
                 Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                 var result = regex.Match(s);
                 return result.Success;
             })
           .WithMessage("Please enter valid email adress");
        RuleFor(s => s.Phone)
            .NotNull()
            .WithMessage("Phone not be null")
            .NotEmpty()
            .WithMessage("Phone not be empty")
             .Must(s =>
             {
                 Regex regex = new Regex(@"^(\+994|0)(50|51|55|70|77|99)[1-9]\d{6}$");
                 var result = regex.Match(s);
                 return result.Success;
             })
           .WithMessage("Please enter valid Phone number");
        RuleFor(s => s.Location)
             .NotNull()
            .WithMessage("Location not be null")
            .NotEmpty()
            .WithMessage("Location not be empty")
            .MinimumLength(5)
            .WithMessage("Location length must be greather than 5");
        RuleFor(s => s.HeaderLogoFile)
            .SetValidator(new FileValidator())
            .WithMessage("HeaderLogo type or size is not valid");
        RuleFor(s => s.FooterLogoFile)
            .SetValidator(new FileValidator())
            .WithMessage("FooterLogo type or size is not valid");
    }
}