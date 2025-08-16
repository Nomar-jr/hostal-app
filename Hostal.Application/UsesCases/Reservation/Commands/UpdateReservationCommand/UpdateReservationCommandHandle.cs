using AutoMapper;
using Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;

public class UpdateReservationCommandHandle(
    ILogger<UpdateReservationCommandHandle> logger,
    IRepository<Domain.Entities.Reservation> repositoryReservation,
    IRepository<Domain.Entities.Client> repositoryClient,
    IMapper mapper): IRequestHandler<UpdateReservationCommand>
{
    public async Task Handle(UpdateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating new reservation with identifier {request.Id} ", request.Id);
        
        var client = await repositoryClient.GetByIdAsync(request.ClientId, cancellationToken) ??
                     throw new NotFoundException(nameof(Domain.Entities.Client), request.ClientId.ToString());
        
        var daysCount = request.EndDateReservation.Day - request.StartDateReservation.Day;
        request.TotalAmount = client.IsVip
            ? (daysCount * 10) * (decimal)0.9
            : (daysCount * 10);
        
        var reservation = await repositoryReservation.GetByIdAsync(request.Id, cancellationToken) ??
                          throw new NotFoundException(nameof(Domain.Entities.Reservation), request.Id.ToString());

        if (reservation.IsClientInHostel)
        {
            throw new ClientIsInHostelException();
        }
        
        var reservationToUpdate = mapper.Map(request, reservation);
        
        await repositoryReservation.UpdateAsync(reservationToUpdate, cancellationToken);
        await repositoryReservation.SaveChangesAsync(cancellationToken);
    }
}