using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.DeleteHeadHouseKeeperCommand;

public class DeleteHeadHouseKeeperCommandHandle(
    ILogger<DeleteHeadHouseKeeperCommandHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository): IRequestHandler<DeleteHeadHouseKeeperCommand>
{
    public async Task Handle(DeleteHeadHouseKeeperCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting headhousekeeper with identifier {request.Id}");
        var headhousekeeper = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                              throw new NotFoundException(nameof(Domain.Entities.HeadHousekeeper),
                                  request.Id.ToString());
        headhousekeeper.IsActive = false;
        await repository.SaveChangesAsync(cancellationToken);
    }
}