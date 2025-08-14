using Ardalis.Specification;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;
using Microsoft.EntityFrameworkCore;

namespace Hostal.Application.Specifications.Room.GetRoomById;

public sealed class GetRoomById: Specification<Domain.Entities.Room, RoomQueryDto>
{
    public GetRoomById(int id)
    {
        Query.Where(x => x.Id == id).Include(x => x.RoomHeadHousekeepers)
            .ThenInclude(x => x.HeadHousekeeper)
            .Include(x => x.Reservations);
        Query.Select(x => new RoomQueryDto()
        {
            Id = x.Id,
            Number = x.Number,
            Capacity = x.Capacity,
            IsOutOfService = x.IsOutOfService,
            IsActive = x.IsActive,
            HeadHousekeepers = x.RoomHeadHousekeepers.Where(z => z.HeadHousekeeper.IsActive)
                .Select(c => new HeadHousekeeperAssignmentDto()
            {
                Id = c.HeadHousekeeperId,
                NameHeadHousekeeper = c.HeadHousekeeper.Name
            }).ToList()
        });
    }
}