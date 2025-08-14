using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Client.DTOs.QueriesDto;

public class ClientQueryDto
{
    /// <summary>
    /// Gets or sets the unique identifier for the client.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client has VIP status.
    /// VIP clients may be eligible for special benefits or discounts.
    /// </summary>
    public bool IsVip { get; set; }
    
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
    /// Gets or sets the email address of the client.
    /// This is used for communication and reservation confirmations.
    /// </summary>
    public string? Email { get; set; }
    
    /// <summary>
    /// Property for SoftDelete
    /// </summary>
    public bool IsActive { get; set; }
    
    /// <summary>
    /// Navigation property for Reservation
    /// </summary>
    public List<Domain.Entities.Reservation> Reservations { get; set; } = [];
}