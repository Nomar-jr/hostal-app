using System.Text.Json.Serialization;

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
    /// Gets or sets the date and time of the reservation.
    /// </summary>
    public DateTime ReservationDate { get; set; }
    
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
    /// Gets or sets if the client is in hostel.
    /// </summary>
    public bool IsClientInHostel { get; set; }

    /// <summary>
    /// Gets or sets if the reservation is canceled by client.
    /// </summary>
    public bool IsCanceled { get; set; }
    
    /// <summary>
    /// Date when the reservation was canceled.
    /// </summary>
    public DateTime? CancellationDate { get; set; }
    
    /// <summary>
    /// Reason for cancellation.
    /// </summary>
    public string? CancellationReason { get; set; }
    
    /// <summary>
    /// Gets or sets the foreign key to the client who made the reservation.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the client who made the reservation.
    /// Navigation property to the <see cref="Client"/> entity.
    /// </summary>
    [JsonIgnore]
    public Client Client { get; set; }

    /// <summary>
    /// Gets or sets the foreign key to the reserved room.
    /// </summary>
    public int RoomId { get; set; }
    
    /// <summary>
    /// Gets or sets the room that is reserved.
    /// Navigation property to the <see cref="Room"/> entity.
    /// </summary>
    [JsonIgnore]
    public Room Room { get; set; }
}