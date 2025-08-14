namespace Hostal.Application.UsesCases.Reservation.Commands;
using FluentValidation;
using DTOs.CommandsDto;

public class BaseReservationCommandValidator : AbstractValidator<ReservationCommandDto>
{
    public BaseReservationCommandValidator()
    {
        // Regla para StartDateReservation: no debe estar vacía.
        RuleFor(r => r.StartDateReservation)
            .NotEmpty().WithMessage("La fecha de inicio de la reserva es obligatoria.");

        // Regla para EndDateReservation: no debe estar vacía.
        RuleFor(r => r.EndDateReservation)
            .NotEmpty().WithMessage("La fecha de fin de la reserva es obligatoria.");

        // Regla para TotalAmount: debe ser mayor que 0.
        RuleFor(r => r.TotalAmount)
            .GreaterThan(0).WithMessage("El monto total debe ser mayor que cero.");

        // Regla para ClientId: debe ser mayor que 0.
        RuleFor(r => r.ClientId)
            .GreaterThan(0).WithMessage("El ID del cliente no es válido.");

        // Regla para RoomId: debe ser mayor que 0.
        RuleFor(r => r.RoomId)
            .GreaterThan(0).WithMessage("El ID de la habitación no es válido.");

        // Regla personalizada para validar que EndDateReservation sea al menos 3 días mayor que StartDateReservation.
        RuleFor(r => r.EndDateReservation)
            .Must((dto, endDate) => endDate.Date >= dto.StartDateReservation.Date.AddDays(3))
            .WithMessage("La fecha de finalización debe ser al menos 3 días posterior a la fecha de inicio.");
    }
}