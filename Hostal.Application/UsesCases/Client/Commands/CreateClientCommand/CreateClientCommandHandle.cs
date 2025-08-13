using AutoMapper;
using Hostal.Application.UsesCases.Client.DTOs;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;

public class CreateClientCommandHandle(
    ILogger<CreateClientCommandHandle> logger,
    IRepository<Domain.Entities.Client> repository,
    IMapper mapper) : IRequestHandler<CreateClientCommand>
{
    public async Task Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new client: {@ClienRequest}", request);
        var clientMapped = mapper.Map<Domain.Entities.Client>(request);
        await repository.AddAsync(clientMapped, cancellationToken);
    }
}