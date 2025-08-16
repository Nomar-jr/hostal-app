using AutoMapper;
using Hostal.Application.Specifications.Room.GetReservationsRoomPast;
using Hostal.Application.UsesCases.Room.DTOs.RoomReservationPastDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetRoomReservationsPast;

public record class GetRoomReservationsPast : IRequest<List<RoomReservationPastDto>>
{
    public int Id { get; set; }
}
    

public class GetRoomReservationsPastHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper): IRequestHandler<GetRoomReservationsPast, List<RoomReservationPastDto>>
{
    public async Task<List<RoomReservationPastDto>> Handle(GetRoomReservationsPast request,
        CancellationToken cancellationToken)
        => mapper.Map<List<RoomReservationPastDto>>(await repository.ListAsync(new GetReservationsRoomPast(request.Id),
            cancellationToken));
}