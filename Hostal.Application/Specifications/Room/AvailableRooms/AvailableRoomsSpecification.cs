using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.AvailableRooms;

public sealed class AvailableRoomsSpecification : Specification<Domain.Entities.Room>
{
    public AvailableRoomsSpecification(DateTime startDate, DateTime endDate)
    {
        Query
            .Where(room => room.IsActive && !room.IsOutOfService)
            .Where(room => !room.Reservations.Any(reservation => 
                !reservation.IsCanceled && 
                !(endDate < reservation.StartDateReservation || 
                  startDate > reservation.EndDateReservation)))
            .Include(room => room.Reservations)
            .OrderBy(room => room.Number);
    }
}