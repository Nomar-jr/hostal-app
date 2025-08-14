using AutoMapper;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.DeleteReservationCommand;

public class DeleteReservationCommandHandle(ILogger<DeleteReservationCommandHandle> logger, IRepository<Domain.Entities.Reservation> repository): IRequestHandler<DeleteReservationCommand> 
{
    public async Task Handle(DeleteReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting a reservation with identifier {request.Id} ", request.Id);
        var reservation = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                          throw new NotFoundException(nameof(Domain.Entities.Reservation), request.Id.ToString());
        reservation.IsCanceled = true;
        reservation.CancellationReason = request.Comment;
        await repository.SaveChangesAsync(cancellationToken);        
        
    }
}