using AutoMapper;
using Hostal.Application.Specifications.Reservation.GetAllReservations;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetAllReservationsQuery;

public record class GetAllReservationsQuery : IRequest<List<ReservationQueryDto>>;

public sealed class GetAllReservationsQueryHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper): IRequestHandler<GetAllReservationsQuery, List<ReservationQueryDto>>
{
    public async Task<List<ReservationQueryDto>> Handle(GetAllReservationsQuery request, CancellationToken cancellationToken)
    => mapper.Map<List<ReservationQueryDto>>(await repository.ListAsync(new GetAllReservations(), cancellationToken));
}