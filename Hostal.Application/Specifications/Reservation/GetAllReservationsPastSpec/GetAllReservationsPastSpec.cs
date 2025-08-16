using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace Hostal.Application.Specifications.Reservation.GetAllReservationsPastSpec;

public sealed class GetAllReservationsPastSpec: Specification<Domain.Entities.Reservation>
{
    public GetAllReservationsPastSpec(int pageNumber, int pageSize)
    {
        Query.Where(x => x.StartDateReservation < DateTime.UtcNow && x.IsCanceled == false)
            .Include(x => x.Client)
            .Include(r => r.Room);
        Query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Id);
    }
}