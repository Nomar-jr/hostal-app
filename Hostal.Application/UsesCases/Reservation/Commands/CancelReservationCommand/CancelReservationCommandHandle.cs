using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.CancelReservationCommand;

public class CancelReservationCommandHandle(ILogger<CancelReservationCommandHandle> logger, IRepository<Domain.Entities.Reservation> repository): IRequestHandler<CancelReservationCommand>
{
    public async Task Handle(CancelReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Cancelling reservation with identifier {request.Id}");
        var reservation = await repository.GetByIdAsync(request.Id, cancellationToken)
                          ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.Id.ToString());
        reservation.IsCanceled = true;
        reservation.CancellationDate = DateTime.Now;
        reservation.CancellationReason = request.CancellationReason;
        await repository.SaveChangesAsync(cancellationToken);
    }
}