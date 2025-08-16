using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.GetReservationRoomActive;

public sealed class GetReservationRoomActive: Specification<Domain.Entities.Reservation>
{
    public GetReservationRoomActive(int roomId)
    {
        Query.Where(x => x.RoomId == roomId && x.StartDateReservation >= DateTime.UtcNow)
            .Include(x => x.Client);
    }
}