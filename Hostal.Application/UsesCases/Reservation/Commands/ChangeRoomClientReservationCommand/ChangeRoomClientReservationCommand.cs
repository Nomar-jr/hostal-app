using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.ChangeRoomClientReservationCommand;

public class ChangeRoomClientReservationCommand: IRequest
{
    public int ReservationId { get; set; }
    public int RoomId { get; set; }
}