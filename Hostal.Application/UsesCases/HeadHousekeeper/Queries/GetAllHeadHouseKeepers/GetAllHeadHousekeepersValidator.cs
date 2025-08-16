using FluentValidation;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;

public class GetAllHeadHousekeepersValidator: AbstractValidator<GetAllHeadHouseKeeperQuery>
{
    public GetAllHeadHousekeepersValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("El número de página debe ser mayor que 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100)
            .WithMessage("El tamaño de página de comprender entre 1 y 100");
    }
}