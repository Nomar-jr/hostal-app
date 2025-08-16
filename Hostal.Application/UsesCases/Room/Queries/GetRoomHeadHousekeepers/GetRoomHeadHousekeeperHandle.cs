using AutoMapper;
using Hostal.Application.Specifications.Room.RoomHeadHousekeepersByRoomId;
using Hostal.Application.UsesCases.Room.DTOs.RoomHeadHousekeepersDto;
using Hostal.Domain.Entities;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Room.Queries.GetRoomHeadHousekeepers;

public record GetRoomHeadHousekeepersQuery: IRequest<List<RoomHeadHousekeepersDto>>
{
    public int RoomId { get; set; }
}


public class GetRoomHeadHousekeepersQueryHandler(
    IRepository<RoomHeadHousekeeper> roomHeadHousekeeperRepository,
    IRepository<Domain.Entities.Room> roomRepository,
    IMapper mapper,
    ILogger<GetRoomHeadHousekeepersQueryHandler> logger) 
    : IRequestHandler<GetRoomHeadHousekeepersQuery, List<RoomHeadHousekeepersDto>>
{
    public async Task<List<RoomHeadHousekeepersDto>> Handle(
        GetRoomHeadHousekeepersQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting HeadHousekeepers for Room ID: {RoomId}", request.RoomId);

        // 1. Verify that the room exists and is active
        var room = await roomRepository.GetByIdAsync(request.RoomId, cancellationToken);
        if (room == null)
        {
            logger.LogWarning("Room with ID {RoomId} not found", request.RoomId);
            throw new NotFoundException(nameof(Domain.Entities.Room), request.RoomId.ToString());
        }

        if (!room.IsActive)
        {
            logger.LogWarning("Room with ID {RoomId} is inactive", request.RoomId);
            return new List<RoomHeadHousekeepersDto>(); // Return empty list for inactive rooms
        }

        // 2. Get the room-headhousekeeper relationships
        var spec = new RoomHeadHousekeepersByRoomIdSpec(request.RoomId);
        var roomHeadHousekeepers = await roomHeadHousekeeperRepository.ListAsync(spec, cancellationToken);

        // 3. Map to DTOs
        var result = mapper.Map<List<RoomHeadHousekeepersDto>>(roomHeadHousekeepers);

        logger.LogInformation("Retrieved {Count} HeadHousekeepers for Room {RoomId}", 
            result.Count, request.RoomId);
        return result;
    }
}