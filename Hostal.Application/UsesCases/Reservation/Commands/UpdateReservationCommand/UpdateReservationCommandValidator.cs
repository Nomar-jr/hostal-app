using FluentValidation;

namespace Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;

public class UpdateReservationCommandValidator: AbstractValidator<UpdateReservationCommand>
{
    public UpdateReservationCommandValidator()
    {
        Include(new BaseReservationCommandValidator());
    }
}