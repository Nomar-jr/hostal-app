using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;

public class RoomQueryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the room.
    /// </summary>
    public int Id { get; set; }
    
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
    /// Properties for HeadHousekeeper
    /// </summary>
    public List<HeadHousekeeperAssignmentDto> HeadHousekeepers { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of reservations made for this room.
    /// Tracks all past and upcoming bookings.
    /// </summary>
    public List<Domain.Entities.Reservation> Reservations { get; set; } = [];
}