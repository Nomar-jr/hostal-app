namespace Hostal.Domain.Entities;

/// <summary>
/// Represents a booking made by a client for a specific room during a specified time period.
/// Handles reservation details including dates, pricing, and related entities.
/// </summary>
public class Reservation
{
    /// <summary>
    /// Gets or sets the unique identifier for the reservation.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the start date and time of the reservation.
    /// </summary>
    public DateTime StartDateReservation { get; set; }
    
    /// <summary>
    /// Gets or sets the end date and time of the reservation.
    /// </summary>
    public DateTime EndDateReservation { get; set; }
    
    /// <summary>
    /// Gets or sets the total cost of the reservation.
    /// This may include room charges and any additional services.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the client who made the reservation.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the client who made the reservation.
    /// Navigation property to the <see cref="Client"/> entity.
    /// </summary>
    public Client Client { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the reserved room.
    /// </summary>
    public int RoomId { get; set; }
    
    /// <summary>
    /// Gets or sets the room that is reserved.
    /// Navigation property to the <see cref="Room"/> entity.
    /// </summary>
    public Room Room { get; set; }
}