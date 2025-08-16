using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.GetReservationsRoomPast;

public sealed class GetReservationsRoomPast: Specification<Domain.Entities.Reservation>
{
    public GetReservationsRoomPast(int roomId)
    {
        Query.Where(p => p.RoomId == roomId && p.StartDateReservation < DateTime.UtcNow).Include(x => x.Client);
    }
}