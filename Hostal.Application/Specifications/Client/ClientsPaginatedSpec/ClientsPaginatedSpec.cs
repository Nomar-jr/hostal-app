using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.ClientsPaginatedSpec;

public sealed class ClientsPaginatedSpec : Specification<Domain.Entities.Client>
{
    public ClientsPaginatedSpec(int pageNumber, int pageSize)
    {
        Query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Id);
    }
}