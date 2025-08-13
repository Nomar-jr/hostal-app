using FluentValidation;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;

public class CreateHeadHouseKeeperCommandValidator: AbstractValidator<CreateHeadHouseKeeperCommand>
{
    public CreateHeadHouseKeeperCommandValidator()
    {
        Include(new BaseHeadHouseKeeperValidator());
    }
}