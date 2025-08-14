using MediatR;

namespace Hostal.Application.UsesCases.Room.Commands.DeleteRoomCommand;

public class DeleteRoomCommand(int id): IRequest
{
    public int Id { get;} = id;
}