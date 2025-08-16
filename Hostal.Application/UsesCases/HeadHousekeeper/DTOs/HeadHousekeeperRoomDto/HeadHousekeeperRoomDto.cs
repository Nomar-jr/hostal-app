namespace Hostal.Application.UsesCases.HeadHousekeeper.DTOs.HeadHousekeeperRoomDto;

public class HeadHousekeeperRoomDto
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
}