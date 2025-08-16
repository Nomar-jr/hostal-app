using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Commands.RoomOutServiceCommand;

public class RoomOutServiceCommandHandle(IRepository<Domain.Entities.Room> repository): IRequestHandler<RoomOutServiceCommand>
{
    public async Task Handle(RoomOutServiceCommand request, CancellationToken cancellationToken)
    {
        var room = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                   throw new NotFoundException(nameof(Domain.Entities.Room), request.Id.ToString());
        room.IsOutOfService = true;
        await repository.SaveChangesAsync(cancellationToken);
    }
}