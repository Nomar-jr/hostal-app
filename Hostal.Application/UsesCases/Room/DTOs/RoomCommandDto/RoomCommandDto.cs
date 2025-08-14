using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Room.DTOs.RoomCommandDto;

public class RoomCommandDto
{
    /// <summary>
    /// Gets or sets the room number or identifier.
    /// This is typically a string to accommodate various numbering schemes (e.g., '101', '2A', 'Penthouse').
    /// </summary>
    public string Number { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the maximum number of occupants the room can accommodate.
    /// </summary>
    public int Capacity { get; set; }
    
    /// <summary>
    /// Property for if the room is out of service
    /// </summary>
    public bool IsOutOfService { get; set; }
    
    /// <summary>
    /// Property for SoftDelete
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of head housekeepers responsible for this room.
    /// A room may have multiple head housekeepers assigned to it.
    /// </summary>
    public List<int> HeadHousekeeperIds { get; set; } = [];
}