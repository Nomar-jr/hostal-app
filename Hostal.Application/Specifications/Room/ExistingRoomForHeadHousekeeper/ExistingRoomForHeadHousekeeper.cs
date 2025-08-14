using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.ExistingRoomForHeadHousekeeper;

public sealed class ExistingRoomForHeadHousekeeper: Specification<Domain.Entities.Room>
{
    public ExistingRoomForHeadHousekeeper(IEnumerable<int> roomIds)
    {
        Query.Where(x => roomIds.Contains(x.Id) && x.IsActive);
    }
}