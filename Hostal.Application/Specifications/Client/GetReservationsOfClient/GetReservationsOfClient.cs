using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;

namespace Hostal.Application.Specifications.Client.GetReservationsOfClient;

public sealed class GetReservationsOfClient: Specification<Domain.Entities.Reservation>
{
    public GetReservationsOfClient(int clientId)
    {
        Query.Where(x => x.ClientId == clientId && x.IsCanceled == false && x.StartDateReservation >= DateTime.UtcNow)
            .Include(x => x.Room);
    }
}