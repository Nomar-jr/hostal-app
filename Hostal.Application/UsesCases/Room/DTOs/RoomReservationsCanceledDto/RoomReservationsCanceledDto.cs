namespace Hostal.Application.UsesCases.Room.DTOs.RoomReservationsCanceledDto;

public class RoomReservationsCanceledDto
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
    /// Date when the reservation was canceled.
    /// </summary>
    public DateTime? CancellationDate { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string ClientName { get; set; } = default!;
}