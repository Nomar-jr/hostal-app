using AutoMapper;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Commands.CheckClientReservationCommand;

public class CheckClientReservationCommandHandle(IRepository<Domain.Entities.Reservation> repository): IRequestHandler<CheckClientReservationCommand>
{
    public async Task Handle(CheckClientReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await repository.GetByIdAsync(request.Id, cancellationToken)
                          ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.Id.ToString());
        reservation.IsClientInHostel = true;
        await repository.SaveChangesAsync(cancellationToken);
    }
}