using Hostal.Domain.Entities.AbstractClass;

namespace Hostal.Domain.Entities;

/// <summary>
/// Represents a client of the hotel who can make reservations.
/// Inherits from <see cref="Person"/> to include personal information.
/// </summary>
public class Client: Person
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
    /// Gets or sets the email address of the client.
    /// This is used for communication and reservation confirmations.
    /// </summary>
    public string? Email { get; set; }
}