using Hostal.Application.UsesCases.Client.DTOs;
using Hostal.Application.UsesCases.Client.DTOs.CommandsDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using MediatR;

namespace Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;

public class UpdateClientCommand(int id) : ClientCommandDto, IRequest
{
    public int Id { get; set; }
}