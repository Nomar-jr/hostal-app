using Ardalis.Specification;

namespace Hostal.Application.Specifications.RoomHeadHousekeeper;

public sealed class RoomHeadHousekeepersByHeadHousekeeperId: Specification<Domain.Entities.RoomHeadHousekeeper>
{
    public RoomHeadHousekeepersByHeadHousekeeperId(int headHousekeeperId)
    {
        Query.Where(x => x.HeadHousekeeperId == headHousekeeperId);
    }
}