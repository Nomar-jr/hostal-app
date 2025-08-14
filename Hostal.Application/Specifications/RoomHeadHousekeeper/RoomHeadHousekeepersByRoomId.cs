using Ardalis.Specification;

namespace Hostal.Application.Specifications.RoomHeadHousekeeper;

public sealed class RoomHeadHousekeepersByRoomId: Specification<Domain.Entities.RoomHeadHousekeeper>
{
    public RoomHeadHousekeepersByRoomId(int id)
    {
        Query.Where(x => x.RoomId == id);
    }
}