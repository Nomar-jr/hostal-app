using MediatR;

namespace Hostal.Application.UsesCases.Room.Commands.RoomOutServiceCommand;

public class RoomOutServiceCommand: IRequest
{
    public int Id { get; set; }
}