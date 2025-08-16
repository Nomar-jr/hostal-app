using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.RoomHeadHousekeepersByRoomId;

/// <summary>
/// Specification to get RoomHeadHousekeeper relationships by Room ID with HeadHousekeeper details
/// </summary>
public sealed class RoomHeadHousekeepersByRoomIdSpec : Specification<Domain.Entities.RoomHeadHousekeeper>
{
    public RoomHeadHousekeepersByRoomIdSpec(int roomId)
    {
        Query.Where(rhh => rhh.RoomId == roomId);
        
        // Include HeadHousekeeper details and filter by active HeadHousekeepers
        Query.Include(rhh => rhh.HeadHousekeeper)
            .Where(rhh => rhh.HeadHousekeeper.IsActive);
        
        // Order by assignment date (most recent first), then by HeadHousekeeper name
        Query.OrderByDescending(rhh => rhh.AssignedDate)
            .ThenBy(rhh => rhh.HeadHousekeeper.LastName)
            .ThenBy(rhh => rhh.HeadHousekeeper.Name);
    }
}