using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.ReservationsActiveCountSpec;

public sealed class ReservationsActiveCountSpec: Specification<Domain.Entities.Reservation>
{
    public ReservationsActiveCountSpec()
    {
        Query.Where(x => x.IsCanceled == false && x.StartDateReservation >= DateTime.Now);
    }
}