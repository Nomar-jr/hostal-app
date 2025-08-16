using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.ClientsCountSpec;

public sealed class ClientsCountSpec: Specification<Domain.Entities.Client>
{
    public ClientsCountSpec()
    {
        Query.Where(client => client.IsActive == true);
    }
}