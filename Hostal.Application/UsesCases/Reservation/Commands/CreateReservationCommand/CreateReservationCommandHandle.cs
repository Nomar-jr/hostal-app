using AutoMapper;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;

public class CreateReservationCommandHandle(
    ILogger<CreateReservationCommandHandle> logger,
    IRepository<Domain.Entities.Reservation> repository,
    IMapper mapper) : IRequestHandler<CreateReservationCommand>
{
    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new reservation");
        var reservation = mapper.Map<Domain.Entities.Reservation>(request);
        await repository.AddAsync(reservation, cancellationToken);
    }
}