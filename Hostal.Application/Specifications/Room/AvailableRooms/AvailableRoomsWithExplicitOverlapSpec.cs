using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.AvailableRooms;

public sealed class AvailableRoomsWithExplicitOverlapSpec : Specification<Domain.Entities.Room>
{
    public AvailableRoomsWithExplicitOverlapSpec(DateTime startDate, DateTime endDate)
    {
        Query
            .Where(room => room.IsActive)
            .Where(room => !room.IsOutOfService)
            .Where(room => !room.Reservations.Any(reservation => 
                !reservation.IsCanceled && 
                reservation.StartDateReservation <= endDate && 
                reservation.EndDateReservation >= startDate))
            .Include(room => room.Reservations)
            .OrderBy(room => room.Number);
    }
}