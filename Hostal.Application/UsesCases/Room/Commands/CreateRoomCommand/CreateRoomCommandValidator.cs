using FluentValidation;

namespace Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;

public class CreateRoomCommandValidator: AbstractValidator<CreateRoomCommand>
{
    public CreateRoomCommandValidator()
    {
        Include(new BaseRoomCommandValidator());
    }
}