using AutoMapper;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientQueryDto;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Queries.GetClientByIdQuery;

public record class GetClientByIdQuery : IRequest<ClientQueryDto>
{
    public int Id { get; set; }
}

public sealed class GetClientByIdQueryHandle(ILogger<GetClientByIdQueryHandle> logger, IRepository<Domain.Entities.Client> repository, IMapper mapper): IRequestHandler<GetClientByIdQuery, ClientQueryDto>
{
    public async Task<ClientQueryDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Search client with identifier {request.Id}");
        return mapper.Map<ClientQueryDto>(await repository.GetByIdAsync(request.Id, cancellationToken) ??
                                          throw new NotFoundException(nameof(Domain.Entities.Client), request.Id.ToString()));
    }
}