using FluentValidation;

namespace Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;

public class UpdateRoomCommandValidator: AbstractValidator<UpdateRoomCommand>
{
    public UpdateRoomCommandValidator()
    {
        Include(new BaseRoomCommandValidator());
    }
}