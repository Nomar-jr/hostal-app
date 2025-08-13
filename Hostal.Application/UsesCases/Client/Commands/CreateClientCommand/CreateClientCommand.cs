using Hostal.Application.UsesCases.Client.DTOs.CommandsDto;
using MediatR;

namespace Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;

public class CreateClientCommand : ClientCommandDto, IRequest;
