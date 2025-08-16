using AutoMapper;
using Hostal.Application.Specifications.Reservation.ActiveReservationsByDateSpec;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ActiveReservationDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetActiveReservationsByDateQuery;

public class GetActiveReservationsByDateQueryHandler(
    IRepository<Domain.Entities.Reservation> repository,
    IMapper mapper)
    : IRequestHandler<GetActiveReservationsByDateQuery, IEnumerable<ActiveReservationDto>>
{
    public async Task<IEnumerable<ActiveReservationDto>> Handle(
        GetActiveReservationsByDateQuery request, 
        CancellationToken cancellationToken)
    {
        var specification = new ActiveReservationsByDateSpec(request.Date);
            
        var reservations = await repository.ListAsync(specification, cancellationToken);
            
        return mapper.Map<IEnumerable<ActiveReservationDto>>(reservations);
    }
}