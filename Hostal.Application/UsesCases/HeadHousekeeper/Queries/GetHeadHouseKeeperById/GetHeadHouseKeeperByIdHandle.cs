using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.GetAllHeadHouseKeeperRoom;
using Hostal.Application.Specifications.HeadHouseKeeper.GetHeadHouseKeeperRoom;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetHeadHouseKeeperById;

public record class GetHeadHouseKeeperQuery : IRequest<HeadHouseKeeperQueryDto>
{
    public int Id { get; set; }
}

public class GetHeadHouseKeeperByIdHandle(
    ILogger<GetHeadHouseKeeperByIdHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper) : IRequestHandler<GetHeadHouseKeeperQuery, HeadHouseKeeperQueryDto>
{
    public async Task<HeadHouseKeeperQueryDto> Handle(GetHeadHouseKeeperQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Search headhousekeeper with identifier {request.Id}");
        return mapper.Map<HeadHouseKeeperQueryDto>(await repository.FirstOrDefaultAsync(new GetHeadHouseKeeperRoom(request.Id), cancellationToken) ??
                                                   throw new NotFoundException(nameof(Domain.Entities.HeadHousekeeper),
                                                       request.Id.ToString()));
    }
}