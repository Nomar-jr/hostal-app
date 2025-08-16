using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.GetAllReservationsCanceled;

public sealed class GetAllReservationsCanceled: Specification<Domain.Entities.Reservation>
{
    public GetAllReservationsCanceled(int pageNumber, int pageSize)
    {
        Query.Where(x => x.IsCanceled == true)
            .Include(x => x.Client)
            .Include(r => r.Room);
        Query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Id);
    }
}