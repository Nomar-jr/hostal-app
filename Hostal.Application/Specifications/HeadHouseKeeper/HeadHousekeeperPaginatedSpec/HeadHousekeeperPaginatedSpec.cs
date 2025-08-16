using Ardalis.Specification;
using Hostal.Domain.Entities;


namespace Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperPaginatedSpec;

public sealed class HeadHousekeeperPaginatedSpec: Specification<HeadHousekeeper>
{
    public HeadHousekeeperPaginatedSpec(int pageNumber, int pageSize)
    {
        Query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Id);
    }
}