using AutoMapper;
using Hostal.Application.Specifications.Client.GetClientReservationsPastSpec;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Client.Queries.GetClientReservationsPastQuery;

public record class GetClientReservationsPastQuery : IRequest<List<ClientReservationDto>>
{
    public int Id { get; set; }
} 

public class GetClientReservationsPastHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper): IRequestHandler<GetClientReservationsPastQuery, List<ClientReservationDto>>
{
    public async Task<List<ClientReservationDto>> Handle(GetClientReservationsPastQuery request,
        CancellationToken cancellationToken)
        => mapper.Map<List<ClientReservationDto>>(
            await repository.ListAsync(new GetClientReservationsPastSpec(request.Id), cancellationToken));


}