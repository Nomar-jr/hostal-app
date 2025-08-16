using Ardalis.Specification;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;

namespace Hostal.Application.Specifications.Room.GetAllRooms;

public sealed class GetAllRooms: Specification<Domain.Entities.Room, RoomQueryDto>
{
    public GetAllRooms()
    {
        Query.Where(x => x.IsActive == true).Include(x => x.RoomHeadHousekeepers).ThenInclude(x => x.HeadHousekeeper);

        Query.Select(x => new RoomQueryDto()
        {
            Id = x.Id,
            Number = x.Number,
            Capacity = x.Capacity,
            IsOutOfService = x.IsOutOfService,
            IsActive = x.IsActive/*,
            HeadHousekeepers = x.RoomHeadHousekeepers.Where(x => x.Room.IsActive && x.HeadHousekeeper.IsActive)
                .Select(x => new HeadHousekeeperAssignmentDto()
                {
                    Id = x.HeadHousekeeperId,
                    NameHeadHousekeeper = x.HeadHousekeeper.Name
                }).ToList()*/
        });
    }
}