using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.CommandsDto;
using MediatR;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;

public class UpdateHeadHouseKeeperCommand(int id): HeadHouseKeeperCommandDto,IRequest
{
    public int Id { get; set; }
}