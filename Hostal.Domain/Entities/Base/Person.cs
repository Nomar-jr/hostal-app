namespace Hostal.Domain.Entities.Base;

public abstract class Person
{
    /// <summary>
    /// Gets or sets the unique identifier for the person.
    /// </summary>
    public int Id { get; set; }
    
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
}