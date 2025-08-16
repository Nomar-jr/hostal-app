using Ardalis.Specification;

namespace Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperRoomByIdSpec;

public sealed class HeadHousekeeperRoomByIdSpec: Specification<Domain.Entities.RoomHeadHousekeeper>
{
    public HeadHousekeeperRoomByIdSpec(int headHousekeeperId)
    {
        Query.Where(x => x.HeadHousekeeperId == headHousekeeperId);
        Query.Include(x => x.Room).Where(x => x.Room.IsActive);
        // Order by assignment date (most recent first), then by HeadHousekeeper name
        Query.OrderByDescending(rhh => rhh.AssignedDate)
            .ThenBy(rhh => rhh.Room.Number);
    }
}