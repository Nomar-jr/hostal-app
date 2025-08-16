using AutoMapper;
using Hostal.Application.Common;
using Hostal.Application.Specifications.Reservation.GetAllReservations;
using Hostal.Application.Specifications.Reservation.ReservationsActiveCountSpec;
using Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetAllHeadHouseKeepers;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationQueryDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsQuery;

public record class GetAllReservationsQuery : IRequest<PagedResult<ReservationQueryDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public sealed class GetAllReservationsQueryHandle(
    IRepository<Domain.Entities.Reservation> repository,
    IMapper mapper,
    ILogger<GetAllHeadHouseKeeperHandle> logger)
    : IRequestHandler<GetAllReservationsQuery, PagedResult<ReservationQueryDto>>
{
    public async Task<PagedResult<ReservationQueryDto>> Handle(GetAllReservationsQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Listing reservations active - Page: {PageNumber}, Size: {PageSize}",
            request.PageNumber, request.PageSize);

        // Validación de parámetros
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        // Si tienes un límite máximo de elementos por página
        pageSize = pageSize > 100 ? 100 : pageSize;

        // Obtener el total de elementos (sin paginación)
        var totalCount = await repository.CountAsync(new ReservationsActiveCountSpec(), cancellationToken);

        // Si no hay elementos, retornar resultado vacío
        if (totalCount == 0)
        {
            return new PagedResult<ReservationQueryDto>(
                Enumerable.Empty<ReservationQueryDto>(),
                0,
                pageNumber,
                pageSize);
        }

        // Obtener los elementos paginados
        var reservations = await repository.ListAsync(
            new GetAllReservations(pageNumber, pageSize),
            cancellationToken);

        // Mapear a DTOs
        var reservationDtos = mapper.Map<IEnumerable<ReservationQueryDto>>(reservations);

        // Retornar resultado paginado
        return new PagedResult<ReservationQueryDto>(
            reservationDtos,
            totalCount,
            pageNumber,
            pageSize);
    }
}