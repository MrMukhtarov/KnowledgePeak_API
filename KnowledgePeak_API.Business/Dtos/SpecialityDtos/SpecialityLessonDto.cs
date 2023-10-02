using KnowledgePeak_API.Business.Dtos.LessonDtos;

namespace KnowledgePeak_API.Business.Dtos.SpecialityDtos;

public record SpecialityLessonDto
{
    public LessonListItemDto Lesson { get; set; }
}
