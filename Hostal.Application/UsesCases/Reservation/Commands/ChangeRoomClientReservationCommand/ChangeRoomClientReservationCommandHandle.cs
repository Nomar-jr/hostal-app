using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Commands.ChangeRoomClientReservationCommand;

public class ChangeRoomClientReservationCommandHandle(ILogger<ChangeRoomClientReservationCommandHandle> logger,IRepository<Domain.Entities.Reservation> repository): IRequestHandler<ChangeRoomClientReservationCommand>
{
    public async Task Handle(ChangeRoomClientReservationCommand request, CancellationToken cancellationToken)
    {
        var reservation = await repository.GetByIdAsync(request.ReservationId, cancellationToken)
            ?? throw new NotFoundException(nameof(Domain.Entities.Reservation), request.ReservationId.ToString());
        
        logger.LogInformation($"Change the room client with identifier {reservation.ClientId} to the room with identifier {request.RoomId}");
        
        reservation.RoomId = request.RoomId;
        await repository.SaveChangesAsync(cancellationToken);
    }
}