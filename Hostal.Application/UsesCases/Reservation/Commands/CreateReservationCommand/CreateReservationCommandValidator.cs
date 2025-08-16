using FluentValidation;
using Hostal.Application.Specifications.Reservation.ClientOverlappingReservationsSpec;
using Hostal.Domain.Interfaces;

namespace Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;

public class CreateReservationCommandValidator: AbstractValidator<CreateReservationCommand>
{
    public CreateReservationCommandValidator(IRepository<Domain.Entities.Reservation> reservationRepository)
    {
        Include(new BaseReservationCommandValidator());
    }
}