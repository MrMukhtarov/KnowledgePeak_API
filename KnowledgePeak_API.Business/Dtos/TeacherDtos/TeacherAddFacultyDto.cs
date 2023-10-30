using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherAddFacultyDto
{
    public IEnumerable<int>? FacultyIds { get; set; }
}
public class TeacherAddFacultyDtoValidator : AbstractValidator<TeacherAddFacultyDto>
{
    public TeacherAddFacultyDtoValidator()
    {
        RuleFor(s => s.FacultyIds)
         .Must(s => IsDistinct(s))
         .WithMessage("Id can not be repeated");
    }
    private bool IsDistinct(IEnumerable<int> ids)
    {
        var encounteredIds = new HashSet<int>();

        if (ids != null)
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