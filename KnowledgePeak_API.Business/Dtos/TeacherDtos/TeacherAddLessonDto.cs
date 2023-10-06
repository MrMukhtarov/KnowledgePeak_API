using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherAddLessonDto
{
    public List<int> LessonIds { get; set; }
}
public class TeacherAddLessonDtoValidator : AbstractValidator<TeacherAddLessonDto>
{
    public TeacherAddLessonDtoValidator()
    {
        RuleFor(s => s.LessonIds)
        .Must(s => IsDistinct(s))
        .WithMessage("Id can not be repeated");
    }
    private bool IsDistinct(IEnumerable<int> ids)
    {
        var encounteredIds = new HashSet<int>();

        foreach (var id in ids)
        {
            if (encounteredIds.Contains(id)) return false;
            encounteredIds.Add(id);
        }

        return true;
    }
}
