using Hostal.Application.UsesCases.Room.DTOs.RoomCommandDto;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;

public class CreateRoomCommand : RoomCommandDto, IRequest<int>;
