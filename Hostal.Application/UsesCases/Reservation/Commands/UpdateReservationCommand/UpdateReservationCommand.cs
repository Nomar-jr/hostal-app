using Hostal.Application.UsesCases.Reservation.DTOs.CommandsDto;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;

public class UpdateReservationCommand(int id): ReservationCommandDto, IRequest
{
    public int Id { get; set; }
}