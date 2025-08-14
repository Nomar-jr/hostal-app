using Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;
using Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.Queries.GetAllClientsQuery;
using Hostal.Application.UsesCases.Client.Queries.GetClientByIdQuery;
using Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.DeleteReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsQuery;
using Hostal.Application.UsesCases.Reservation.Queries.GetReservationByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ReservationController(IMediator mediator) : Controller
{
    [HttpGet("GetAllReservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<ReservationQueryDto>>> GetAllReservations() =>
        Ok(await  mediator.Send(new GetAllReservationsQuery()));

    [HttpGet("GetReservationById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ReservationQueryDto>> GetReservationByIdQuery([FromRoute] GetReservationByIdQuery query) =>
        Ok(await mediator.Send(query));
    
    
    
    [HttpPost("CreateReservation")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateReservation([FromBody] CreateReservationCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }


    [HttpPut("UpdateReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateReservation([FromRoute] int id, UpdateReservationCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("DeleteReservation/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteReservation([FromRoute] int id, [FromBody] DeleteReservationCommand command)
    {
        await mediator.Send(new DeleteReservationCommand(id));
        return NoContent();
    }
}