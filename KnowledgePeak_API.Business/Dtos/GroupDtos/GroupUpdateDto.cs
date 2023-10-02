using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupUpdateDto
{
    public string Name { get; set; }
    public int Limit { get; set; }
    public int SpecialityId { get; set; }
}
public class GroupUpdateDtoValidator : AbstractValidator<GroupUpdateDto>
{
    public GroupUpdateDtoValidator()
    {
        RuleFor(g => g.Name)
          .NotNull()
          .WithMessage("Group Naem not be null")
          .NotEmpty()
          .WithMessage("Group Name not be Empty")
          .MinimumLength(2)
          .WithMessage("Group Name length must be grather than 2");
        RuleFor(g => g.Limit)
        .NotEmpty()
        .WithMessage("Group Name not be Empty")
        .NotNull()
        .WithMessage("Group Naem not be null")
        .GreaterThan(0)
        .WithMessage("Group Limit must be grather than 0");
        RuleFor(g => g.SpecialityId)
          .NotNull()
          .WithMessage("Speciality id not be null")
          .NotEmpty()
          .WithMessage("Speciality id not be empty")
          .GreaterThan(0)
          .WithMessage("Speciality id must be greather than 0");
    }
}
