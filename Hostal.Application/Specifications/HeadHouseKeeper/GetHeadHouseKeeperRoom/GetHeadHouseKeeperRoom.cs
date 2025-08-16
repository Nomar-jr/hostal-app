using Ardalis.Specification;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.GetHeadHouseKeeperRoom;

public sealed class GetHeadHouseKeeperRoom: Specification<HeadHousekeeper, HeadHouseKeeperQueryDto>
{
    public GetHeadHouseKeeperRoom(int id)
    {
        Query.Where(x => x.Id == id).Include(x => x.RoomHeadHousekeepers)
            .ThenInclude(x => x.Room);
        Query.Select(x => new HeadHouseKeeperQueryDto()
        {
            Id = x.Id,
            CI = x.CI,
            Name = x.Name,
            LastName = x.LastName,
            IsActive = x.IsActive,
            Phone = x.Phone/*,
            Rooms = x.RoomHeadHousekeepers.Where(z => z.HeadHousekeeper.IsActive)
                .Select(z => new RoomAssignmentDto()
                {
                    Id = z.RoomId,
                    Number = z.Room.Number
                }).ToList()*/
        });
    }
}