using AutoMapper;
using Hostal.Application.Common;
using Hostal.Application.Specifications.Reservation.GetAllReservationsCanceled;
using Hostal.Application.Specifications.Reservation.ReservationsCanceledCountSpec;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationCanceledQueryDto;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsCanceledQuery;


public record class GetAllReservationsCanceledQuery : IRequest<PagedResult<ReservationCanceledQueryDto>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

public class GetAllReservationsCanceledHandle(
    IRepository<Domain.Entities.Reservation> repository,
    IMapper mapper,
    ILogger<GetAllReservationsCanceledHandle> logger)
    : IRequestHandler<GetAllReservationsCanceledQuery, PagedResult<ReservationCanceledQueryDto>>
{
    public async Task<PagedResult<ReservationCanceledQueryDto>> Handle(GetAllReservationsCanceledQuery request,
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
        var totalCount = await repository.CountAsync(new ReservationsCanceledCountSpec(), cancellationToken);

        // Si no hay elementos, retornar resultado vacío
        if (totalCount == 0)
        {
            return new PagedResult<ReservationCanceledQueryDto>(
                Enumerable.Empty<ReservationCanceledQueryDto>(),
                0,
                pageNumber,
                pageSize);
        }

        // Obtener los elementos paginados
        var reservations = await repository.ListAsync(
            new GetAllReservationsCanceled(pageNumber, pageSize),
            cancellationToken);

        // Mapear a DTOs
        var reservationDtos = mapper.Map<IEnumerable<ReservationCanceledQueryDto>>(reservations);

        // Retornar resultado paginado
        return new PagedResult<ReservationCanceledQueryDto>(
            reservationDtos,
            totalCount,
            pageNumber,
            pageSize);
    }
}