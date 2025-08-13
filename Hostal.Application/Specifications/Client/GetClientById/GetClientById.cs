using Ardalis.Specification;

namespace Hostal.Application.Specifications.Client.GetClientById;

public sealed class GetClientById: Specification<Domain.Entities.Client>
{
    public GetClientById(int id)
    {
        Query.Where(x => x.Id == id);
    }
}