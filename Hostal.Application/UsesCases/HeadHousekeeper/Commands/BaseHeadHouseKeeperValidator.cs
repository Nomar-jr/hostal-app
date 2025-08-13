using FluentValidation;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.CommandsDto;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands;

public class BaseHeadHouseKeeperValidator: AbstractValidator<HeadHouseKeeperCommandDto>
{
    public BaseHeadHouseKeeperValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Ingrese el nombre de la ama de llaves");
        RuleFor(x => x.CI).NotEmpty().WithMessage("Ingrese el carnet de identidad de la ama de llaves");
        RuleFor(x => x.Phone).NotEmpty().WithMessage("Ingrese el número de teléfono de la ama de llaves");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Ingrese los apellidos de la ama de llaves");
    }
}