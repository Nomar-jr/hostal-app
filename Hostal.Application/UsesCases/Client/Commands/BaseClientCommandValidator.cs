using FluentValidation;
using Hostal.Application.UsesCases.Client.DTOs.CommandsDto;

namespace Hostal.Application.UsesCases.Client.Commands;

public class BaseClientCommandValidator: AbstractValidator<ClientCommandDto>
{
    public BaseClientCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Ingrese el nombre del cliente");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Ingrese los apellidos del cliente");
        RuleFor(x => x.CI).NotEmpty().WithMessage("Ingrese el carnet de identidad del cliente");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Ingrese el número de teléfono del cliente");
    }
}