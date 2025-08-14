using Hostal.Application.UsesCases.Room.DTOs.RoomCommandDto;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;

public class UpdateRoomCommand(int id): RoomCommandDto,IRequest
{
    public int Id { get; set; }
}