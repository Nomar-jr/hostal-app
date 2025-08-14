using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.DeleteReservationCommand;

public class DeleteReservationCommand(int id): IRequest
{
    public int Id { get;} = id;
    public string Comment { get; set; } = default!;
}