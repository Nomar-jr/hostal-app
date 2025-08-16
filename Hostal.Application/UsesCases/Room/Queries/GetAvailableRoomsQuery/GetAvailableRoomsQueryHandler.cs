using AutoMapper;
using Hostal.Application.Specifications.Room.AvailableRooms;
using Hostal.Application.UsesCases.Room.DTOs.AvailableRoomDto;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetAvailableRoomsQuery;

public class GetAvailableRoomsQueryHandler(IRepository<Domain.Entities.Room> repository, IMapper mapper): IRequestHandler<GetAvailableRoomsQuery, List<AvailableRoomDto>>
{
    public async Task<List<AvailableRoomDto>> Handle(GetAvailableRoomsQuery request, CancellationToken cancellationToken)
    {
        var specification = new AvailableRoomsSpecification(request.StartDate, request.EndDate);
            
        var availableRooms = await repository.ListAsync(specification, cancellationToken);

        return mapper.Map<List<AvailableRoomDto>>(availableRooms);
    }
}