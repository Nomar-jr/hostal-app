using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;

public class DeleteClientCommandHandle(
    ILogger<DeleteClientCommandHandle> logger,
    IRepository<Domain.Entities.Client> repository) : IRequestHandler<DeleteClientCommand>
{
    public async Task Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting client with identifier {request.Id}");
        var client = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                     throw new NotFoundException(nameof(Domain.Entities.Client), request.Id.ToString());
        client.IsActive = false;
        await repository.SaveChangesAsync(cancellationToken);
    }
}