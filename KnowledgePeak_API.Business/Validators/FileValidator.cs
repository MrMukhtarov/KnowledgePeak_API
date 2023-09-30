using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace KnowledgePeak_API.Business.Validators;

public class FileValidator : AbstractValidator<IFormFile>
{
    public FileValidator(int mb = 3, string contentType = "image")
    {
        RuleFor(f => f.ContentType)
            .Must(f => f.Contains(contentType))
            .WithMessage("File Fomrat is wrong");
        RuleFor(f => f.Length)
            .LessThanOrEqualTo(mb * 1024 * 1024)
            .WithMessage("File max size must be " + mb * 1024 * 1024);
    }
}
