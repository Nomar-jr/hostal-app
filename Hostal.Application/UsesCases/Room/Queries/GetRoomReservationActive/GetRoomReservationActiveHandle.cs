using AutoMapper;
using Hostal.Application.Specifications.Room.GetReservationRoomActive;
using Hostal.Application.UsesCases.Room.DTOs.RoomReservationActiveDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetRoomReservationActive;

public record class GetRoomReservationActive : IRequest<List<RoomReservationActiveDto>>
{
    public int Id { get; set; }
}

public class GetRoomReservationActiveHandle(
    IRepository<Domain.Entities.Room> repositoryRoom,
    IRepository<Domain.Entities.Reservation> repositoryReservation,
    IMapper mapper) : IRequestHandler<GetRoomReservationActive, List<RoomReservationActiveDto>>
{
    public async Task<List<RoomReservationActiveDto>> Handle(GetRoomReservationActive request, CancellationToken cancellationToken)
    => mapper.Map<List<RoomReservationActiveDto>>(await repositoryReservation.ListAsync(new GetReservationRoomActive(request.Id), cancellationToken));
}