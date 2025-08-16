using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.ReservationsCanceledCountSpec;

public sealed class ReservationsCanceledCountSpec: Specification<Domain.Entities.Reservation>
{
    public ReservationsCanceledCountSpec()
    {
        Query.Where(x => x.IsCanceled == true);
    }
}