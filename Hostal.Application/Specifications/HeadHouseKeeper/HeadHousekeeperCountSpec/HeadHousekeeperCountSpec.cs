using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperCountSpec;

public sealed class HeadHousekeeperCountSpec : Specification<HeadHousekeeper>
{
    public HeadHousekeeperCountSpec()
    {
        Query.Where(x => x.IsActive == true);
    }
}