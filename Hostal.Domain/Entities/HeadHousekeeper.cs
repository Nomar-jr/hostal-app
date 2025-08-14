using System.Text.Json.Serialization;
using Hostal.Domain.Entities.AbstractClass;

namespace Hostal.Domain.Entities;

/// <summary>
/// Represents a head housekeeper responsible for managing rooms in the hotel.
/// Inherits from <see cref="Person"/> to include personal information.
/// </summary>
public class HeadHousekeeper: Person
{
    /// <summary>
    /// Gets or sets the unique identifier for the head housekeeper.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Property for SoftDelete
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of rooms that this head housekeeper is responsible for.
    /// A head housekeeper may be responsible for multiple rooms.
    /// </summary>
    [JsonIgnore]
    public ICollection<RoomHeadHousekeeper> RoomHeadHousekeepers { get; set; } = [];
}