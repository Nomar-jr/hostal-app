using AutoMapper;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.CommandsDto;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;

public class UpdateHeadHouseKeeperCommandHandle(
    ILogger<CreateHeadHouseKeeperCommandHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper): IRequestHandler<UpdateHeadHouseKeeperCommand>
{
    public async Task Handle(UpdateHeadHouseKeeperCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating headhousekeeper with identifier {request.Id}");
        var headhousekeeper = await repository.GetByIdAsync(request.Id, cancellationToken) ??
                              throw new NotFoundException(nameof(Domain.Entities.HeadHousekeeper), request.Id.ToString());
        var headhousekeeperMapped = mapper.Map(request, headhousekeeper);
        await repository.UpdateAsync(headhousekeeperMapped, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);
    }
    
}