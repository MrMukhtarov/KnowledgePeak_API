using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupAddSpecialityDto
{
    public int SpecialityId { get; set; }
}
public class GroupAddSpecialityDtoValidator : AbstractValidator<GroupAddSpecialityDto>
{
    public GroupAddSpecialityDtoValidator()
    {
        RuleFor(g => g.SpecialityId)
            .NotNull()
            .WithMessage("Speciality id not be null")
            .NotEmpty()
            .WithMessage("Speciality id not be empty")
            .GreaterThan(0)
            .WithMessage("Speciality id must be greather than 0");
    }
}