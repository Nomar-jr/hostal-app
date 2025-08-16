using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.CheckClientReservationCommand;

public class CheckClientReservationCommand(int id): IRequest
{
    public int Id { get; } = id;
}