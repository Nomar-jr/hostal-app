using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.Reservation.GetAllReservations;

public sealed class GetAllReservations : Specification<Domain.Entities.Reservation>
{
    public GetAllReservations()
    {
        Query.Where(x => x.IsCanceled == false)
             .Include(x => x.Client)
             .Include(r => r.Room);
    }
}