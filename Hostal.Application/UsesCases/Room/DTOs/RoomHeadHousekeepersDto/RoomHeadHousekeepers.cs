namespace Hostal.Application.UsesCases.Room.DTOs.RoomHeadHousekeepersDto;

public class RoomHeadHousekeepersDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the head housekeeper.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string LastName { get; set; } = default!;
}