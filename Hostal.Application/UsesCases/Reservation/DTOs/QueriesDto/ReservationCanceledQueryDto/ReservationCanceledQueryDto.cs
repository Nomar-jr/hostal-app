namespace Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationCanceledQueryDto;

public class ReservationCanceledQueryDto
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
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int ClientId { get; set; }
    
    /// <summary>
    /// Gets or sets the Carnet de Identidad (CI) of the person.
    /// </summary>
    public string ClientCi { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the first name of the person.
    /// </summary>
    public string ClientName { get; set; } = default!;

    /// <summary>
    /// Gets or sets the last name of the person.
    /// </summary>
    public string ClientLastName { get; set; } = default!;
    
    /// <summary>
    /// Room associated with the reservation.
    /// </summary>
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