using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.GetHeadHouseKeeperRoom;

public sealed class GetHeadHouseKeeperRoom: Specification<HeadHousekeeper>
{
    public GetHeadHouseKeeperRoom(int id)
    {
        Query.Where(x => x.Id == id).Include(x => x.Rooms);
    }
}