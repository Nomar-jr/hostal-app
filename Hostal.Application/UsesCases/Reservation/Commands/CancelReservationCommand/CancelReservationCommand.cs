using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.CancelReservationCommand;

public class CancelReservationCommand(int id, string? reason): IRequest
{
    public int Id { get; } = id;
    public string? CancellationReason { get; } = reason;
}