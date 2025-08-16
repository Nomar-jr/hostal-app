using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.ReservationsPastCountSpec;

public sealed class ReservationsPastCountSpec: Specification<Domain.Entities.Reservation>
{
    public ReservationsPastCountSpec()
    {
        Query.Where(x => x.IsCanceled == false && x.StartDateReservation < DateTime.Now);
    }
}