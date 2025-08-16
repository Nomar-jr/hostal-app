using AutoMapper;
using Hostal.Application.Specifications.Client.GetReservationOfClientCanceled;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationCanceledDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Client.Queries.GetClientReservationCanceled;

public record class GetClientReservationCanceled : IRequest<List<ClientReservationCanceledDto>>
{
    public int Id { get; set; }
}

public class GetClientReservationCanceledHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper) : IRequestHandler<GetClientReservationCanceled, List<ClientReservationCanceledDto>>
{
    public async Task<List<ClientReservationCanceledDto>> Handle(GetClientReservationCanceled request, CancellationToken cancellationToken)
        => mapper.Map<List<ClientReservationCanceledDto>>(await repository.ListAsync(new GetReservationOfClientCanceled(request.Id),
                cancellationToken));
}