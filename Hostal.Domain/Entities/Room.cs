namespace Hostal.Domain.Entities;

/// <summary>
/// Represents a room in the hotel that can be reserved by clients.
/// Tracks room details, capacity, and related entities.
/// </summary>
public class Room
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
    /// Gets or sets the collection of head housekeepers responsible for this room.
    /// A room may have multiple head housekeepers assigned to it.
    /// </summary>
    public List<HeadHousekeeper> HeadHousekeeper { get; set; } = [];
    
    /// <summary>
    /// Gets or sets the collection of reservations made for this room.
    /// Tracks all past and upcoming bookings.
    /// </summary>
    public List<Reservation> Reservations { get; set; } = [];
}