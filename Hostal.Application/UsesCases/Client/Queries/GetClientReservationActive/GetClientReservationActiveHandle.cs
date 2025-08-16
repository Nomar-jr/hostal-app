using AutoMapper;
using Hostal.Application.Specifications.Client.GetReservationsOfClient;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Client.Queries.GetClientReservationActive;

public record class GetClientReservationActive : IRequest<List<ClientReservationDto>>
{
    public int Id { get; set; }
}

public class GetClientReservationActiveHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper)
    : IRequestHandler<GetClientReservationActive, List<ClientReservationDto>>
{
    public async Task<List<ClientReservationDto>> Handle(GetClientReservationActive request,
        CancellationToken cancellationToken) =>
        mapper.Map<List<ClientReservationDto>>(await repository.ListAsync(new GetReservationsOfClient(request.Id),
            cancellationToken));
}