using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TutorDtos;

public record TutorAddGroupDto
{
    public string userName { get; set; }
    public List<int>? GroupIds { get; set; }
}
public class TutorAddGroupDtoValidator : AbstractValidator<TutorAddGroupDto>
{
    public TutorAddGroupDtoValidator()
    {
        RuleFor(t => t.GroupIds)
              .Must(s => IsDistinct(s))
            .WithMessage("Id can not be repeated");

    }
    private bool IsDistinct(IEnumerable<int> ids)
    {
        var encounteredIds = new HashSet<int>();

       if(ids != null)
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
