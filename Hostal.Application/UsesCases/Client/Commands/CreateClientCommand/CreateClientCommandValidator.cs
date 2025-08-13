using FluentValidation;

namespace Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;

public class CreateClientCommandValidator: AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        Include(new BaseClientCommandValidator());
    }
}