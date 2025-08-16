using AutoMapper;
using Hostal.Application.Specifications.Room;
using Hostal.Application.Specifications.Room.GetAllRooms;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetAllRoomsQuery;

public record class GetAllRoomsQuery : IRequest<List<RoomQueryDto>>;

public class GetAllRoomsQueryHandle(IRepository<Domain.Entities.Room> repository, IMapper mapper): IRequestHandler<GetAllRoomsQuery, List<RoomQueryDto>>
{
    public async Task<List<RoomQueryDto>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
    => mapper.Map<List<RoomQueryDto>>(await repository.ListAsync(new GetAllRooms() ,cancellationToken));
    
}