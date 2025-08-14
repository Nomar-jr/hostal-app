using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.GetAllHeadHouseKeeperRoom;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;

public record class GetAllHeadHouseKeeperQuery : IRequest<List<HeadHouseKeeperQueryDto>>;

public sealed class GetAllHeadHouseKeeperHandle(
    ILogger<GetAllHeadHouseKeeperHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper) : IRequestHandler<GetAllHeadHouseKeeperQuery, List<HeadHouseKeeperQueryDto>>
{
    public async Task<List<HeadHouseKeeperQueryDto>> Handle(GetAllHeadHouseKeeperQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing all headhousekeepers");
        return await repository.ListAsync(new GetAllHeadHouseKeeperRoom() ,cancellationToken);
    }
}