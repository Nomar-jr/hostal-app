using FluentValidation;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetActiveReservationsByDateQuery;

public class GetActiveReservationsByDateQueryValidator : AbstractValidator<GetActiveReservationsByDateQuery>
{
    public GetActiveReservationsByDateQueryValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("La fecha es requerida")
            .Must(BeAValidDate)
            .WithMessage("Debe ser una fecha válida");
    }

    private bool BeAValidDate(DateTime date)
    {
        return date != default(DateTime);
    }
}