using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityAddLessonDto
{
    public List<int>? LessonIds { get; set; }
}
public class SpecialityAddLessonDtoValidator : AbstractValidator<SpecialityAddLessonDto>
{
    public SpecialityAddLessonDtoValidator()
    {
        RuleFor(s => s.LessonIds)
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
