using AutoMapper;
using Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;

public class CreateHeadHouseKeeperCommandHandle(
    ILogger<CreateHeadHouseKeeperCommandHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper) : IRequestHandler<CreateHeadHouseKeeperCommand>
{
    public async Task Handle(CreateHeadHouseKeeperCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new headhousekeeper");
        var headHouseKeeperMapped = mapper.Map<Domain.Entities.HeadHousekeeper>(request); 
        await repository.AddAsync(headHouseKeeperMapped, cancellationToken);
    }
}