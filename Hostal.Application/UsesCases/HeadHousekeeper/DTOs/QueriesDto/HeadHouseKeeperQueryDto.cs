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
    /// Property for SoftDelete
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Fullname for HeadHousekeeper
    /// </summary>
    public string FullName => $"{Name} {LastName}";
    
    /*/// <summary>
    /// Count about all rooms assigned to head housekeeper.
    /// </summary>
    public int AssignedRoomsCount { get; set; }*/
    
    /*/// <summary>
    /// Rooms assigned to this head housekeeper
    /// </summary>
    public List<RoomAssignmentDto> Rooms { get; set; } = [];*/
}