using AutoMapper;
using Hostal.Application.Specifications.Reservation.GetReservationById;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ReservationQueryDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetReservationByIdQuery;

public record class GetReservationByIdQuery: IRequest<ReservationQueryDto>
{
    public int Id { get; set; }
}

public class GetReservationByIdQueryHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper)
    : IRequestHandler<GetReservationByIdQuery, ReservationQueryDto>
{
    public async Task<ReservationQueryDto> Handle(GetReservationByIdQuery request, CancellationToken cancellationToken)
    => mapper.Map<ReservationQueryDto>(await repository.FirstOrDefaultAsync(new GetReservationById(request.Id), cancellationToken));
}