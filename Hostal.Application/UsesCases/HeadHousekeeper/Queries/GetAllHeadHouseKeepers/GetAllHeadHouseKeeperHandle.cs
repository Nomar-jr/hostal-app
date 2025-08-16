using AutoMapper;
using Hostal.Application.Common;
using Hostal.Application.Specifications.HeadHouseKeeper.GetAllHeadHouseKeeperRoom;
using Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperCountSpec;
using Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperPaginatedSpec;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;

public record class GetAllHeadHouseKeeperQuery : IRequest<PagedResult<HeadHouseKeeperQueryDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public sealed class GetAllHeadHouseKeeperHandle(
    ILogger<GetAllHeadHouseKeeperHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repository,
    IMapper mapper) : IRequestHandler<GetAllHeadHouseKeeperQuery, PagedResult<HeadHouseKeeperQueryDto>>
{
    public async Task<PagedResult<HeadHouseKeeperQueryDto>> Handle(GetAllHeadHouseKeeperQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing headhousekeeper - Page: {PageNumber}, Size: {PageSize}", 
            request.PageNumber, request.PageSize);

        // Validación de parámetros
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;
        
        // Si tienes un límite máximo de elementos por página
        pageSize = pageSize > 100 ? 100 : pageSize;

        // Obtener el total de elementos (sin paginación)
        var totalCount = await repository.CountAsync(new HeadHousekeeperCountSpec(), cancellationToken);

        // Si no hay elementos, retornar resultado vacío
        if (totalCount == 0)
        {
            return new PagedResult<HeadHouseKeeperQueryDto>(
                Enumerable.Empty<HeadHouseKeeperQueryDto>(),
                0,
                pageNumber,
                pageSize);
        }

        // Obtener los elementos paginados
        var headHouseKeepers = await repository.ListAsync(
            new HeadHousekeeperPaginatedSpec(pageNumber, pageSize), 
            cancellationToken);

        // Mapear a DTOs
        var headHouseKeepersDtos = mapper.Map<IEnumerable<HeadHouseKeeperQueryDto>>(headHouseKeepers);

        // Retornar resultado paginado
        return new PagedResult<HeadHouseKeeperQueryDto>(
            headHouseKeepersDtos,
            totalCount,
            pageNumber,
            pageSize);
    }
}