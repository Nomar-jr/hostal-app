using Hostal.Domain.Constant;
using Hostal.Domain.Entities;

namespace Hostal.Domain.Interfaces.Auth;

/// <summary>
/// Interface defining the authorization service for reservation operations.
/// </summary>
public interface IReservationAuthorizationService
{
    /// <summary>
    /// Verifies whether a user is authorized to perform a specific operation on a reservation.
    /// </summary>
    /// <param name="reservation">The reservation on which the operation is to be performed.</param>
    /// <param name="operation">The operation to be performed.</param>
    /// <returns>True if the operation is authorized, otherwise false.</returns>
    bool Authorize(Reservation reservation, ResourceOperation operation);
}