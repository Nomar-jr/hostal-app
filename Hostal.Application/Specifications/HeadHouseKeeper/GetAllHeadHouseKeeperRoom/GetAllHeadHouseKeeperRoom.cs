using Ardalis.Specification;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.GetAllHeadHouseKeeperRoom;

public sealed class GetAllHeadHouseKeeperRoom: Specification<HeadHousekeeper, HeadHouseKeeperQueryDto>
{
    public GetAllHeadHouseKeeperRoom()
    {
        Query.Where(x => x.IsActive == true).Include(x => x.RoomHeadHousekeepers)
            .ThenInclude(x => x.Room);
        Query.Select(x =>  new HeadHouseKeeperQueryDto()
        {
            Id = x.Id,
            CI = x.CI,
            Name = x.Name,
            LastName = x.LastName,
            Phone = x.Phone,
            IsActive = x.IsActive,
            /*Rooms = x.RoomHeadHousekeepers.Where(y => y.Room.IsActive).Select(x => new RoomAssignmentDto()
            {
                Id = x.RoomId,
                Number = x.Room.Number
            }).ToList()*/
        });
        Query.OrderBy(x => x.LastName).ThenBy(x => x.Name);
    }
}