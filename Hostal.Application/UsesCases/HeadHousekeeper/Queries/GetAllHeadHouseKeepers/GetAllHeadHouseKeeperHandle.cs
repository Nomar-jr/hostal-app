using AutoMapper;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;

public record class GetAllHeadHouseKeeper : IRequest<List<HeadHouseKeeperQueryDto>>;

public sealed class GetAllHeadHouseKeeperHandle(
    ILogger<GetAllHeadHouseKeeperHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper) : IRequestHandler<GetAllHeadHouseKeeper, List<HeadHouseKeeperQueryDto>>
{
    public async Task<List<HeadHouseKeeperQueryDto>> Handle(GetAllHeadHouseKeeper request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing all headhousekeepers");
        return mapper.Map<List<HeadHouseKeeperQueryDto>>(await repository.ListAsync(cancellationToken));
    }
}