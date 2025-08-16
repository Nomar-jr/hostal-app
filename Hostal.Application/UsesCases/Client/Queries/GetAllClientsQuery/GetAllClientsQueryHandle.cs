using AutoMapper;
using Hostal.Application.Common;
using Hostal.Application.Specifications.Client.ClientsCountSpec;
using Hostal.Application.Specifications.Client.ClientsPaginatedSpec;
using Hostal.Application.Specifications.Client.GetAllClientsActive;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientQueryDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Client.Queries.GetAllClientsQuery;

public record class GetAllClientsQuery : IRequest<PagedResult<ClientQueryDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public sealed class GetAllClientsQueryHandle(
    ILogger<GetAllClientsQueryHandle> logger,
    IRepository<Domain.Entities.Client> repository,
    IMapper mapper) : IRequestHandler<GetAllClientsQuery, PagedResult<ClientQueryDto>>
{
    public async Task<PagedResult<ClientQueryDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing clients - Page: {PageNumber}, Size: {PageSize}", 
            request.PageNumber, request.PageSize);

        // Validación de parámetros
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        
        // Si tienes un límite máximo de elementos por página
        pageSize = pageSize > 100 ? 100 : pageSize;

        // Obtener el total de elementos (sin paginación)
        var totalCount = await repository.CountAsync(new ClientsCountSpec(), cancellationToken);

        // Si no hay elementos, retornar resultado vacío
        if (totalCount == 0)
        {
            return new PagedResult<ClientQueryDto>(
                Enumerable.Empty<ClientQueryDto>(),
                0,
                pageNumber,
                pageSize);
        }

        // Obtener los elementos paginados
        var clients = await repository.ListAsync(
            new ClientsPaginatedSpec(pageNumber, pageSize), 
            cancellationToken);

        // Mapear a DTOs
        var clientDtos = mapper.Map<IEnumerable<ClientQueryDto>>(clients);

        // Retornar resultado paginado
        return new PagedResult<ClientQueryDto>(
            clientDtos,
            totalCount,
            pageNumber,
            pageSize);
    }
        
}