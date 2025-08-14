using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.GetReservationById;

public sealed class GetReservationById: Specification<Domain.Entities.Reservation>
{
    public GetReservationById(int id)
    {
        Query.Where(x => x.Id == id)
             .Include(x => x.Client)
             .Include(r => r.Room);
    }
}