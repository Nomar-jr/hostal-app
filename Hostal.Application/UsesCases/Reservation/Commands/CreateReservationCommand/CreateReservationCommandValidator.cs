using FluentValidation;

namespace Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;

public class CreateReservationCommandValidator: AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator()
    {
        Include(new BaseReservationCommandValidator());
    }
}