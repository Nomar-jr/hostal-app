using AutoMapper;
using Hostal.Application.Specifications.Client.GetAllClientsActive;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Queries.GetAllClientsQuery;

public record class GetAllClientsQuery: IRequest<List<ClientQueryDto>>;

public sealed class GetAllClientsQueryHandle(
    ILogger<GetAllClientsQueryHandle> logger,
    IRepository<Domain.Entities.Client> repository,
    IMapper mapper) : IRequestHandler<GetAllClientsQuery, List<ClientQueryDto>>
{
    public async Task<List<ClientQueryDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing all clients");
        return mapper.Map<List<ClientQueryDto>>(
            await repository.ListAsync(new GetAllClientsActive(), cancellationToken));
    }
        
}