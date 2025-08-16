using AutoMapper;
using Hostal.Application.Specifications.Room.GetReservationsRoomCanceled;
using Hostal.Application.UsesCases.Room.DTOs.RoomReservationsCanceledDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetRoomReservationsCanceled;

public record class GetRoomReservationsCanceled : IRequest<List<RoomReservationsCanceledDto>>
{
    public int Id { get; set; }
}

public class GetRoomReservationsCanceledHandle(IRepository<Domain.Entities.Reservation> repository, IMapper mapper)
    : IRequestHandler<GetRoomReservationsCanceled, List<RoomReservationsCanceledDto>>
{
    public async Task<List<RoomReservationsCanceledDto>> Handle(GetRoomReservationsCanceled request,
        CancellationToken cancellationToken)
        => mapper.Map<List<RoomReservationsCanceledDto>>(await repository.ListAsync(new GetReservationsRoomCanceled(request.Id) ,cancellationToken));
}