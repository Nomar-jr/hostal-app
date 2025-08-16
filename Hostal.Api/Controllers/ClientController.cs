using Hostal.Application.Common;
using Hostal.Application.Specifications.Client.GetClientById;
using Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;
using Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientQueryDto;
using Hostal.Application.UsesCases.Client.Queries.GetAllClientsQuery;
using Hostal.Application.UsesCases.Client.Queries.GetClientByIdQuery;
using Hostal.Application.UsesCases.Client.Queries.GetClientReservationActive;
using Hostal.Application.UsesCases.Client.Queries.GetClientReservationCanceled;
using Hostal.Application.UsesCases.Client.Queries.GetClientReservationsPastQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ClientController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAllClients")]
    [ProducesResponseType(typeof(PagedResult<ClientQueryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResult<ClientQueryDto>>> GetAllClients(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllClientsQuery
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

    [HttpGet("GetClientById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClientQueryDto>> GetClientByIdQuery([FromRoute] GetClientByIdQuery query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetClientReservationsActiveById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClientQueryDto>> GetClientReservationActiveByIdQuery([FromRoute] GetClientReservationActive query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetClientReservationsCanceledById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClientQueryDto>> GetClientReservationCanceledByIdQuery([FromRoute] GetClientReservationCanceled query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetClientReservationsPastById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ClientQueryDto>> GetClientReservationsPastById([FromRoute] GetClientReservationsPastQuery query) =>
        Ok(await mediator.Send(query));
    
    [HttpPost("CreateClient")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateClient([FromBody] CreateClientCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }


    [HttpPut("UpdateClient/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateClient([FromRoute] int id, UpdateClientCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("DeleteClient/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteClient([FromRoute] int id)
    {
        
        await mediator.Send(new DeleteClientCommand(id));
        return NoContent();
    }

}