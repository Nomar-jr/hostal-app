using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.ExistingHeadHousekeeperforRoom;

public sealed class ExistingHeadHousekeeperforRoom: Specification<HeadHousekeeper>
{
    public ExistingHeadHousekeeperforRoom(IEnumerable<int> headHousekeeperIds)
    {
        Query.Where(x => headHousekeeperIds.Contains(x.Id));
    }
}