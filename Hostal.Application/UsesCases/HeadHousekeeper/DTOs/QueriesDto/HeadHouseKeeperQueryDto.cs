using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;

public class HeadHouseKeeperQueryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the head housekeeper.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the Carnet de Identidad (CI) of the person.
    /// </summary>
    public string CI { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string Name { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string LastName { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the contact phone number of the person.
    /// </summary>
    public string Phone { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the collection of rooms that this head housekeeper is responsible for.
    /// A head housekeeper may be responsible for multiple rooms.
    /// </summary>
    public List<Room> Rooms { get; set; } = [];
}