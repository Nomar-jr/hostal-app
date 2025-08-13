using AutoMapper;
using Hostal.Application.Specifications.Client.GetClientById;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;

public class UpdateClientCommandHandle(
    ILogger<UpdateClientCommandHandle> logger,
    IRepository<Domain.Entities.Client> repository,
    IMapper mapper) : IRequestHandler<UpdateClientCommand>
{
    public async Task Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating client with identifier {request.Id}");
        var client = await repository.FirstOrDefaultAsync(new GetClientById(request.Id), cancellationToken) ??
                     throw new NotFoundException(nameof(Domain.Entities.Client), request.Id.ToString());
        var clientToUpdate = mapper.Map(request, client);
        await repository.UpdateAsync(clientToUpdate, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
}