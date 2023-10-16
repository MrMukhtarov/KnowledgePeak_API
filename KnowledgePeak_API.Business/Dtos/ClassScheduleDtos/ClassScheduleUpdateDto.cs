using FluentValidation;
using KnowledgePeak_API.Core.Enums;

namespace KnowledgePeak_API.Business.Dtos.ClassScheduleDtos;

public record ClassScheduleUpdateDto
{
    public DateTime ScheduleDate { get; set; }
    public Days Day { get; set; }
    public int GroupId { get; set; }
    public int LessonId { get; set; }
    public int ClassTimeId { get; set; }
    public int RoomId { get; set; }
    public string TeacherId { get; set; }
}
public class ClassScheduleUpdateDtoValidator : AbstractValidator<ClassScheduleUpdateDto>
{
    public ClassScheduleUpdateDtoValidator()
    {
        RuleFor(c => c.ScheduleDate)
           .NotNull()
           .WithMessage("Scheduledate not be null")
           .NotEmpty()
           .WithMessage("ScheduleDate not be empty");
        RuleFor(c => c.Day)
            .NotNull()
            .WithMessage("Schedule Day  not be Null");
        RuleFor(c => c.GroupId)
            .NotNull()
            .WithMessage("Scheduledate GroupId not be null")
            .NotEmpty()
            .WithMessage("ScheduleDate GroupId not be empty")
            .GreaterThan(0)
            .WithMessage("GroupId must be grather than 0");
        RuleFor(c => c.LessonId)
            .NotNull()
            .WithMessage("LessonId not be null")
            .NotEmpty()
            .WithMessage("LessonId not be empty")
            .GreaterThan(0)
            .WithMessage("LessonId must be grather than 0");
        RuleFor(c => c.ClassTimeId)
            .NotNull()
            .WithMessage("ClassTimeId not be null")
            .NotEmpty()
            .WithMessage("ClassTimeId not be empty")
            .GreaterThan(0)
            .WithMessage("ClassTimeId must be grather than 0");
        RuleFor(c => c.RoomId)
            .NotNull()
            .WithMessage("Scheduledate not be null")
            .NotEmpty()
            .WithMessage("ScheduleDate not be empty")
            .GreaterThan(0)
            .WithMessage("ClassTimeId must be grather than 0");
        RuleFor(c => c.TeacherId)
           .NotNull()
           .WithMessage("TeacherId not be null")
           .NotEmpty()
           .WithMessage("TeacherId not be empty");
    }
}
