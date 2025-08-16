using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.GetClientReservationsPastSpec;

public sealed class GetClientReservationsPastSpec: Specification<Domain.Entities.Reservation>
{
    public GetClientReservationsPastSpec(int clientId)
    {
        Query.Where(x =>
            x.ClientId == clientId && x.StartDateReservation < DateTime.UtcNow && x.IsCanceled == false).Include(x => x.Room);
    }
}