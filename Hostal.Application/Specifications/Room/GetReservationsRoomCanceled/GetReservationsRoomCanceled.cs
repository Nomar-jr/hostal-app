using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.GetReservationsRoomCanceled;

public sealed class GetReservationsRoomCanceled: Specification<Domain.Entities.Reservation>
{
    public GetReservationsRoomCanceled(int roomId)
    {
        Query.Where(x => x.RoomId == roomId && x.IsCanceled == true).Include(x => x.Client);
    }
}