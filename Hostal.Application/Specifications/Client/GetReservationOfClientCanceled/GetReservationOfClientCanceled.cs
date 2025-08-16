using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.GetReservationOfClientCanceled;

public sealed class GetReservationOfClientCanceled: Specification<Domain.Entities.Reservation>
{
    public GetReservationOfClientCanceled(int clientId)
    {
        Query.Where(x => x.ClientId == clientId && x.IsCanceled == true).Include(x => x.Room);
    }
}