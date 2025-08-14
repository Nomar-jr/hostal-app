using AutoMapper;
using Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;

public class UpdateReservationCommandHandle(
    ILogger<UpdateReservationCommandHandle> logger,
    IRepository<Domain.Entities.Reservation> repository,
    IMapper mapper): IRequestHandler<UpdateReservationCommand>
{
    public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating new reservation with identifier {request.Id} ", request.Id);
        var reservation = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                          throw new NotFoundException(nameof(Domain.Entities.Reservation), request.Id.ToString());
        var reservationToUpdate = mapper.Map(request, reservation);
        await repository.UpdateAsync(reservationToUpdate, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}