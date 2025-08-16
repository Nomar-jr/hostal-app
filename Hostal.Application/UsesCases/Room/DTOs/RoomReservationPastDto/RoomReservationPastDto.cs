namespace Hostal.Application.UsesCases.Room.DTOs.RoomReservationPastDto;

public class RoomReservationPastDto
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
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string ClientName { get; set; } = default!;
}