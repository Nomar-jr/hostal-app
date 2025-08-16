using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperRoomByIdSpec;
using Hostal.Application.Specifications.Room.RoomHeadHousekeepersByRoomId;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.HeadHousekeeperRoomDto;
using Hostal.Application.UsesCases.Room.DTOs.RoomHeadHousekeepersDto;
using Hostal.Domain.Entities;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Queries.GetHeadHousekeeperRoom;

public record class GetHeadHousekeeperRoomQuery : IRequest<List<HeadHousekeeperRoomDto>>
{
    public int Id { get; set; }
}

public class GetHeadHousekeeperRoomHandle(
    IRepository<Domain.Entities.HeadHousekeeper> headHousekeeperRepository,
    IRepository<RoomHeadHousekeeper> roomHeadHousekeeperRepository, IMapper mapper,
    ILogger<GetHeadHousekeeperRoomHandle> logger)
    : IRequestHandler<GetHeadHousekeeperRoomQuery, List<HeadHousekeeperRoomDto>>
{
    public async Task<List<HeadHousekeeperRoomDto>> Handle(GetHeadHousekeeperRoomQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting Rooms for HeadHousekeeper ID: {HeadHousekeeperId}", request.Id);

        // 1. Verify that the room exists and is active
        var headHousekeeper = await headHousekeeperRepository.GetByIdAsync(request.Id, cancellationToken);
        if (headHousekeeper == null)
        {
            logger.LogWarning("HeadHousekeeper with ID {HeadHousekeeperId} not found", request.Id);
            throw new NotFoundException(nameof(Domain.Entities.HeadHousekeeper), request.Id.ToString());
        }

        if (!headHousekeeper.IsActive)
        {
            logger.LogWarning("HeadHousekeeper with ID {HeadHousekeeperId} is inactive", request.Id);
            return []; // Return empty list for inactive rooms
        }

        // 2. Get the room-headhousekeeper relationships
        var spec = new HeadHousekeeperRoomByIdSpec(request.Id);
        var roomHeadHousekeepers = await roomHeadHousekeeperRepository.ListAsync(spec, cancellationToken);

        // 3. Map to DTOs
        var result = mapper.Map<List<HeadHousekeeperRoomDto>>(roomHeadHousekeepers);

        logger.LogInformation("Retrieved {Count} Rooms for Room {HeadHousekeeperId}", 
            result.Count, request.Id);
        return result;    
    }
}