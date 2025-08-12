namespace Hostal.Domain.Exceptions;

/// <summary>
/// Exception that is thrown when a requested resource is not found.
/// </summary>
public class NotFoundException(string resourceType, string resourceIdentifier)
    : Exception($"{resourceType} with identifier '{resourceIdentifier}' was not found");