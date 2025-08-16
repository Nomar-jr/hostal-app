using Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;
using Hostal.Application.UsesCases.Room.Commands.DeleteRoomCommand;
using Hostal.Application.UsesCases.Room.Commands.RoomOutServiceCommand;
using Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;
using Hostal.Application.UsesCases.Room.DTOs.AvailableRoomDto;
using Hostal.Application.UsesCases.Room.DTOs.RoomHeadHousekeepersDto;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;
using Hostal.Application.UsesCases.Room.Queries.GetAllRoomsQuery;
using Hostal.Application.UsesCases.Room.Queries.GetAvailableRoomsQuery;
using Hostal.Application.UsesCases.Room.Queries.GetRoomById;
using Hostal.Application.UsesCases.Room.Queries.GetRoomHeadHousekeepers;
using Hostal.Application.UsesCases.Room.Queries.GetRoomReservationActive;
using Hostal.Application.UsesCases.Room.Queries.GetRoomReservationsCanceled;
using Hostal.Application.UsesCases.Room.Queries.GetRoomReservationsPast;
using Hostal.Domain.Exceptions;
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

    /// <summary>
    /// Gets available rooms for the specified date range
    /// </summary>
    /// <param name="startDate">Start date of the reservation period</param>
    /// <param name="endDate">End date of the reservation period</param>
    /// <returns>List of available rooms</returns>
    [HttpGet("GetAvailableRooms")]
    public async Task<ActionResult<List<AvailableRoomDto>>> GetAvailableRooms(
        [FromQuery] DateTime startDate,
        [FromQuery] DateTime endDate)
    {
        var query = new GetAvailableRoomsQuery
        {
            StartDate = startDate,
            EndDate = endDate
        };

        var result = await mediator.Send(query);
        return Ok(result);
    }
    
    
    [HttpGet("GetRoomById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoomQueryDto>> GetRoomById([FromRoute] GetRoomByIdQuery query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetRoomReservationsActive/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoomQueryDto>> GetRoomReservationsActive([FromRoute] GetRoomReservationActive query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetRoomReservationsCanceled/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoomQueryDto>> GetRoomReservationsCanceled([FromRoute] GetRoomReservationsCanceled query) =>
        Ok(await mediator.Send(query));

    [HttpGet("GetHeadHousekeepersByRoom/{RoomId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RoomHeadHousekeepersDto>>> GetHeadHousekeepersByRoom(
        [FromRoute] GetRoomHeadHousekeepersQuery query)
        => Ok(await mediator.Send(query));
    
    [HttpGet("GetRoomReservationsPast/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<RoomQueryDto>> GetRoomReservationsPast([FromRoute] GetRoomReservationsPast query) =>
        Ok(await mediator.Send(query));
    
    [HttpPost("CreateRoom")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateRoom([FromBody] CreateRoomCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }
    
    [HttpPost("RoomOutService/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult> RoomOutService([FromRoute] RoomOutServiceCommand command)
    {
        await mediator.Send(command);
        return Ok();
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