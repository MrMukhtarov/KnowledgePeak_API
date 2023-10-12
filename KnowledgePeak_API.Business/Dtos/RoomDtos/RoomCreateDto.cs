using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.RoomDtos;

public record RoomCreateDto
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public int FacultyId { get; set; }
}
public class RoomCreateDtoValidator : AbstractValidator<RoomCreateDto>
{
    public RoomCreateDtoValidator()
    {
        RuleFor(r => r.RoomNumber)
            .NotNull()
            .WithMessage("Room RoomNumber not be null")
            .NotEmpty()
            .WithMessage("Room RoomNumber not be empty");
        RuleFor(r => r.Capacity)
            .NotNull()
            .WithMessage("Room Capacity not be null")
            .NotEmpty()
            .WithMessage("Room Capacity not be empty")
            .GreaterThan(5)
            .WithMessage("Room Capacity must be grather than 5");
        RuleFor(r => r.FacultyId)
            .GreaterThan(0)
            .WithMessage("Room FacultyId must be grather than 0");
    }
}
