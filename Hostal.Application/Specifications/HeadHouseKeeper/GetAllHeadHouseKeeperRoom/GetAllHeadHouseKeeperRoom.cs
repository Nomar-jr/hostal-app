using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.GetAllHeadHouseKeeperRoom;

public sealed class GetAllHeadHouseKeeperRoom: Specification<HeadHousekeeper>
{
    public GetAllHeadHouseKeeperRoom()
    {
        Query.Where(x => x.IsActive == true).Include(x => x.Rooms);
    }
}