using Hostal.Application.Common;
using Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;
using Hostal.Application.UsesCases.Client.Queries.GetAllClientsQuery;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.DeleteHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetHeadHouseKeeperById;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetHeadHousekeeperRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeadHouseKeeperController(IMediator mediator): ControllerBase
{
    [HttpGet("GetAllHeadHouseKeepers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<HeadHouseKeeperQueryDto>>> GetAllHeadHouseKeepers(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        try
        {
            var query = new GetAllHeadHouseKeeperQuery()
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

    [HttpGet("GetHeadHouseKeeperById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<HeadHouseKeeperQueryDto>> GetHeadHouseKeeperByIdQuery([FromRoute] GetHeadHouseKeeperQuery query) =>
        Ok(await mediator.Send(query));
    
    [HttpGet("GetHeadHouseKeeperRoomById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<HeadHouseKeeperQueryDto>> GetHeadHouseKeeperRoomById([FromRoute] GetHeadHousekeeperRoomQuery query) =>
        Ok(await mediator.Send(query));
    
    [HttpPost("CreateHeadHouseKeeper")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateCategory([FromBody] CreateHeadHouseKeeperCommand command)
    {
        await mediator.Send(command);
        return NoContent();
    }


    [HttpPut("UpdateHeadHouseKeeper/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateCategory([FromRoute] int id, UpdateHeadHouseKeeperCommand command)
    {
        command.Id = id;
        await mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("DeleteHeadHouseKeeper/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> DeleteCategory([FromRoute] int id)
    {
        
        await mediator.Send(new DeleteHeadHouseKeeperCommand(id));
        return NoContent();
    }
}