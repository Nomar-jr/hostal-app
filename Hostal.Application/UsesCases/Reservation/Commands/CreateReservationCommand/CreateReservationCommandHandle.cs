using AutoMapper;
using FluentValidation;
using Hostal.Application.Specifications.Reservation.ClientOverlappingReservationsSpec;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;

public class CreateReservationCommandHandle(
    ILogger<CreateReservationCommandHandle> logger,
    IRepository<Domain.Entities.Reservation> repositoryReservation,
    IRepository<Domain.Entities.Client> repositoryClient,
    IMapper mapper) : IRequestHandler<CreateReservationCommand>
{
    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new reservation");
        
        // Validar que el cliente existe
        var client = await repositoryClient.GetByIdAsync(request.ClientId, cancellationToken) ??
                     throw new NotFoundException(nameof(Domain.Entities.Client), request.ClientId.ToString());

        // Validar que no hay reservas superpuestas para el cliente
        var overlappingSpec = new ClientOverlappingReservationsSpec(
            request.ClientId, 
            request.StartDateReservation, 
            request.EndDateReservation
        );
        
        var overlappingReservations = await repositoryReservation.ListAsync(overlappingSpec, cancellationToken);
        if (overlappingReservations.Any())
        {
            throw new OverlappingReservationsException();
        }

        // Calcular el total
        var daysCount = request.EndDateReservation.Day - request.StartDateReservation.Day;
        request.TotalAmount = client.IsVip
            ? (daysCount * 10) * (decimal)0.9
            : (daysCount * 10);

        // Crear la reserva
        var reservation = mapper.Map<Domain.Entities.Reservation>(request);
        await repositoryReservation.AddAsync(reservation, cancellationToken);
    }
}