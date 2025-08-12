namespace Hostal.Domain.Constant;

/// <summary>
/// Enumeration defining the basic operations that can be performed on a resource.
/// </summary>
public enum ResourceOperation
{
    /// <summary>Resource creation operation.</summary>
    Create, 
    
    /// <summary>Resource read operation.</summary>
    Read,
    
    /// <summary>Resource update operation.</summary>
    Update,
    
    /// <summary>Resource deletion operation.</summary>
    Delete
}