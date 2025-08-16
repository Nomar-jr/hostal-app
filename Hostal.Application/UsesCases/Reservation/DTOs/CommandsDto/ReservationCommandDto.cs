using System.Text.Json.Serialization;
using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Reservation.DTOs.CommandsDto;

/// <summary>
/// DTO base para los comandos de Reservation
/// </summary>
public class ReservationCommandDto
{
    /// <summary>
    /// Gets or sets the date and time when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; set; } = DateTime.Now;
    
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
    [JsonIgnore]
    public decimal TotalAmount { get; set; } = 1;
    
    /// <summary>
    /// Gets or sets if the client is in hostel.
    /// </summary>
    public bool IsClientInHostel { get; set; }
    
    /// <summary>
    /// Gets or sets the foreign key to the client who made the reservation.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the foreign key to the reserved room.
    /// </summary>
    public int RoomId { get; set; }
}
