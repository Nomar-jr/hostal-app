using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.CommandsDto;
using MediatR;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;

public class CreateHeadHouseKeeperCommand: HeadHouseKeeperCommandDto, IRequest;