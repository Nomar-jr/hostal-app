using FluentValidation;
using Hostal.Application.UsesCases.Client.DTOs.CommandsDto;

namespace Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;

public class UpdateClientCommandValidator: AbstractValidator<UpdateClientCommand>
{
    public UpdateClientCommandValidator()
    {
        Include(new BaseCommandValidator());
    }
}