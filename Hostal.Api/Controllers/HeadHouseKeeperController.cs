using Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.DeleteHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetHeadHouseKeeperById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Hostal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HeadHouseKeeperController(IMediator mediator): ControllerBase
{
    [HttpGet("GetAllHeadHouseKeepers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<HeadHouseKeeperQueryDto>>> GetAllHeadHouseKeepers() =>
        Ok(await  mediator.Send(new GetAllHeadHouseKeeperQuery()));

    [HttpGet("GetHeadHouseKeeperById/{Id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<HeadHouseKeeperQueryDto>> GetHeadHouseKeeperByIdQuery([FromRoute] GetHeadHouseKeeperQuery query) =>
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