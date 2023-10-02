using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.GroupDtos;

public record GroupCreateDto
{
    public string Name { get; set; }
    public int Limit { get; set; }
}
public class GroupCreateDtoValidation : AbstractValidator<GroupCreateDto>
{
    public GroupCreateDtoValidation()
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
    }
}
