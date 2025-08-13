using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.GetAllClientsActive;

public sealed class GetAllClientsActive: Specification<Domain.Entities.Client>
{
    public GetAllClientsActive()
    {
        Query.Where(c => c.IsActive).Include(x => x.Reservations);
    }
}