using FluentValidation;
using KnowledgePeak_API.Core.Entities;

namespace KnowledgePeak_API.Business.Dtos.TeacherDtos;

public record TeacherAddSpecialitiyDto
{
    public List<int>? TeacherSpecialities { get; set; }
}
public class TeacherAddSpecialitiyDtoValidator : AbstractValidator<TeacherAddSpecialitiyDto>
{
    public TeacherAddSpecialitiyDtoValidator()
    {
        RuleFor(t => t.TeacherSpecialities)
            .Must(t => IsDistinct(t))
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