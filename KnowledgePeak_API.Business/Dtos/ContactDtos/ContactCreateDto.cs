using FluentValidation;
using System.Text.RegularExpressions;

namespace KnowledgePeak_API.Business.Dtos.ContactDtos;

public record ContactCreateDto
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }
}
public class ContactCreateDtoValidator : AbstractValidator<ContactCreateDto>
{
    public ContactCreateDtoValidator()
    {
        RuleFor(c => c.FullName)
            .NotNull()
            .WithMessage("Name not be null")
            .NotEmpty()
            .WithMessage("Name not be empty");
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
        RuleFor(c => c.Message)
            .NotNull()
            .WithMessage("Message not be null")
            .NotEmpty()
            .WithMessage("Message not be empty");
    }
}
