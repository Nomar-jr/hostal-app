using Hostal.Application.Common;
using Hostal.Application.UsesCases.Reservation.Commands.CancelReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.ChangeRoomClientReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.CheckClientReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.DeleteReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ActiveReservationDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationCanceledQueryDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationQueryDto;
using Hostal.Application.UsesCases.Reservation.Queries.GetActiveReservationsByDateQuery;
using Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsCanceledQuery;
using Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsPastQuery;
using Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsQuery;
using Hostal.Application.UsesCases.Reservation.Queries.GetReservationByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;

/// <summary>
/// Controller responsible for handling all reservation-related operations in the system.
/// Provides endpoints for managing reservations including creation, retrieval, updates, and cancellations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ReservationController(IMediator mediator) : Controller
{
    /// <summary>
    /// Retrieves a paginated list of all active reservations in the system.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination (default is 1).</param>
    /// <param name="pageSize">The number of items per page (default is 10).</param>
    /// <returns>A paginated list of active reservations with status code 200 on success.</returns>
    /// <response code="200">Returns the paginated list of active reservations.</response>
    /// <response code="400">If there was an error processing the request.</response>
    [HttpGet("GetAllReservationsActive")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<ReservationQueryDto>>> GetAllReservationsActive(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllReservationsQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return BadRequest($"Error retrieving clients: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a specific reservation by its unique identifier.
    /// </summary>
    /// <param name="query">The query containing the reservation ID.</param>
    /// <returns>The reservation details if found; otherwise, a 404 Not Found response.</returns>
    /// <response code="200">Returns the requested reservation.</response>
    /// <response code="404">If no reservation is found with the specified ID.</response>
    [HttpGet("GetReservationById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReservationQueryDto>> GetReservationByIdQuery([FromRoute] GetReservationByIdQuery query) =>
        Ok(await mediator.Send(query));

    /// <summary>
    /// Retrieves a paginated list of all canceled reservations in the system.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination (default is 1).</param>
    /// <param name="pageSize">The number of items per page (default is 10).</param>
    /// <returns>A paginated list of canceled reservations with status code 200 on success.</returns>
    /// <response code="200">Returns the paginated list of canceled reservations.</response>
    /// <response code="400">If there was an error processing the request.</response>
    [HttpGet("GetAllReservationsCanceled")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<ReservationCanceledQueryDto>>> GetAllReservationsCanceled(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllReservationsCanceledQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return BadRequest($"Error retrieving clients: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a paginated list of all past reservations in the system.
    /// </summary>
    /// <param name="pageNumber">The page number for pagination (default is 1).</param>
    /// <param name="pageSize">The number of items per page (default is 10).</param>
    /// <returns>A paginated list of past reservations with status code 200 on success.</returns>
    /// <response code="200">Returns the paginated list of past reservations.</response>
    /// <response code="400">If there was an error processing the request.</response>
    [HttpGet("GetAllReservationsPast")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<ReservationQueryDto>>> GetAllReservationsPast(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllReservationsPastQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await mediator.Send(query);
            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception
            return BadRequest($"Error retrieving clients: {ex.Message}");
        }
    }

    /// <summary>
    /// Retrieves a list of active reservations for a specific date.
    /// </summary>
    /// <param name="date">The date for which to retrieve active reservations.</param>
    /// <returns>A list of active reservations for the specified date.</returns>
    /// <response code="200">Returns the list of active reservations.</response>
    /// <response code="400">If there was an error processing the request.</response>
    /// <response code="500">If an internal server error occurs.</response>
    [HttpGet("active-by-date")]
    [ProducesResponseType(typeof(IEnumerable<ActiveReservationDto>), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ActiveReservationDto>>> GetActiveReservationsByDate(
        [FromQuery] DateTime date)
    {
        try
        {
            var query = new GetActiveReservationsByDateQuery(date);
            var result = await mediator.Send(query);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a new reservation with the provided details.
    /// </summary>
    /// <param name="command">The command containing reservation details.</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the reservation was created successfully.</response>
    /// <response code="400">If the reservation data is invalid or an error occurs.</response>
    [HttpPost("CreateReservation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Changes the room assignment for a specific client's reservation.
    /// </summary>
    /// <param name="command">The command containing the reservation ID and new room details.</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the room was changed successfully.</response>
    /// <response code="400">If the request data is invalid or an error occurs.</response>
    /// <response code="404">If the reservation is not found.</response>
    [HttpPost("ChangeRoomClient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> ChangeRoomClient([FromQuery] ChangeRoomClientReservationCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Cancels a specific reservation by its ID.
    /// </summary>
    /// <param name="id">The ID of the reservation to cancel.</param>
    /// <param name="reason">The reason for cancellation (optional).</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the reservation was cancelled successfully.</response>
    /// <response code="404">If the reservation is not found.</response>
    [HttpPost("CancelReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CancelReservation([FromRoute] int id, [FromBody] string? reason)
    {
        await mediator.Send(new CancelReservationCommand(id, reason));
        return NoContent();
    }

    /// <summary>
    /// Checks in a client for a specific reservation.
    /// </summary>
    /// <param name="id">The ID of the reservation to check in.</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the client was checked in successfully.</response>
    /// <response code="404">If the reservation is not found.</response>
    [HttpPost("CheckClientToReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> CheckClientToReservation([FromRoute] int id)
    {
        await mediator.Send(new CheckClientReservationCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Updates the details of an existing reservation.
    /// </summary>
    /// <param name="id">The ID of the reservation to update.</param>
    /// <param name="command">The command containing the updated reservation details.</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the reservation was updated successfully.</response>
    /// <response code="400">If the request data is invalid.</response>
    /// <response code="404">If the reservation is not found.</response>
    [HttpPut("UpdateReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateReservation([FromRoute] int id, UpdateReservationCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    /// <summary>
    /// Deletes a specific reservation from the system.
    /// </summary>
    /// <param name="id">The ID of the reservation to delete.</param>
    /// <param name="command">The delete command (unused parameter, can be removed).</param>
    /// <returns>No content response (204) if successful.</returns>
    /// <response code="204">If the reservation was deleted successfully.</response>
    /// <response code="404">If the reservation is not found.</response>
    [HttpDelete("DeleteReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteReservation([FromRoute] int id, [FromBody] DeleteReservationCommand command)
    {
        await mediator.Send(new DeleteReservationCommand(id));
        return NoContent();
    }
}