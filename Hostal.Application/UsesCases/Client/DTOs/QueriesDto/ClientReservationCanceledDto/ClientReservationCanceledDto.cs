namespace Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationCanceledDto;

public class ClientReservationCanceledDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the reservation.
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the date and time when the reservation was made.
    /// </summary>
    public DateTime ReservationDate { get; set; }
    
    /// <summary>
    /// Date when the reservation was canceled.
    /// </summary>
    public DateTime? CancellationDate { get; set; }
    
    /// <summary>
    /// Gets or sets the start date and time of the reservation.
    /// </summary>
    public DateTime StartDateReservation { get; set; }
    
    /// <summary>
    /// Gets or sets the end date and time of the reservation.
    /// </summary>
    public DateTime EndDateReservation { get; set; }
    
    /// <summary>
    /// Reason for cancellation.
    /// </summary>
    public string? CancellationReason { get; set; }
    
    /// <summary>
    /// Gets or sets the unique identifier for the room.
    /// </summary>
    public int RoomId { get; set; }
    
    /// <summary>
    /// Gets or sets the room number or identifier.
    /// This is typically a string to accommodate various numbering schemes (e.g., '101', '2A', 'Penthouse').
    /// </summary>
    public string RoomNumber { get; set; } = default!;
}