using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;

/// <summary>
/// DTO para las respuestas de las consultas de Reservation
/// </summary>
public class ReservationQueryDto
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
    /// Gets or sets the start date and time of the reservation.
    /// </summary>
    public DateTime StartDateReservation { get; set; }
    
    /// <summary>
    /// Gets or sets the end date and time of the reservation.
    /// </summary>
    public DateTime EndDateReservation { get; set; }
    
    /// <summary>
    /// Gets or sets the number of people for the reservation.
    /// </summary>
    public int NumberPeople { get; set; }
    
    /// <summary>
    /// Gets or sets the total price of the reservation.
    /// </summary>
    public decimal TotalPrice { get; set; }
    
    /// <summary>
    /// Gets or sets the status of the reservation (e.g., Pending, Confirmed, Cancelled).
    /// </summary>
    public string Status { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets any additional comments or special requests for the reservation.
    /// </summary>
    public string? Comments { get; set; }
    
    /// <summary>
    /// Client that made the reservation
    /// </summary>
    
    /// <summary>
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client has VIP status.
    /// VIP clients may be eligible for special benefits or discounts.
    /// </summary>
    public bool ClientIsVip { get; set; }
    
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
    /// Gets or sets the contact phone number of the person.
    /// </summary>
    public string ClientPhone { get; set; } = default!;

    /// <summary>
    /// Gets or sets the email address of the client.
    /// This is used for communication and reservation confirmations.
    /// </summary>
    public string? ClientEmail { get; set; }
    
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
    
    /// <summary>
    /// Gets or sets the maximum number of occupants the room can accommodate.
    /// </summary>
    public int RoomCapacity { get; set; }
}
