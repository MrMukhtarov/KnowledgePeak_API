using FluentValidation;

namespace KnowledgePeak_API.Business.Dtos.RoomDtos;

public record RoomUpdateDto
{
    public string RoomNumber { get; set; }
    public int Capacity { get; set; }
    public int? FacultyId { get; set; }
}
public class RoomUpdateDtoValidator : AbstractValidator<RoomUpdateDto>
{
    public RoomUpdateDtoValidator()
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
    }
}
