using Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;
using Hostal.Application.UsesCases.Room.Commands.DeleteRoomCommand;
using Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;
using Hostal.Application.UsesCases.Room.Queries.GetAllRoomsQuery;
using Hostal.Application.UsesCases.Room.Queries.GetRoomById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController(IMediator mediator) : ControllerBase
{
    [HttpGet("GetAllRooms")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RoomQueryDto>>> GetAllRooms() =>
        Ok(await  mediator.Send(new GetAllRoomsQuery()));

    [HttpGet("GetClientById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoomQueryDto>> GetClientByIdQuery([FromRoute] GetRoomByIdQuery query) =>
        Ok(await mediator.Send(query));
    
    
    
    [HttpPost("CreateRoom")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }


    [HttpPut("UpdateRoom/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateRoom([FromRoute] int id, UpdateRoomCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("DeleteRoom/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteRoom([FromRoute] int id)
    {
        
        await mediator.Send(new DeleteRoomCommand(id));
        return NoContent();
    }
}