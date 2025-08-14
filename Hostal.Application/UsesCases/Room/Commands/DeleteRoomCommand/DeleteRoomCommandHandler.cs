using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Room.Commands.DeleteRoomCommand;

public class DeleteRoomCommandHandler(
    ILogger<DeleteRoomCommandHandler> logger,
    IRepository<Domain.Entities.Room> repository) : IRequestHandler<DeleteRoomCommand>
{
    public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting room with identifier {request.Id}");
        var room = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                   throw new NotFoundException(nameof(Domain.Entities.Room), request.Id.ToString());
        room.IsActive = false;
        await repository.SaveChangesAsync(cancellationToken);
    }
}