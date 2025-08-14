using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperCiAlreadyExist;

public sealed class HeadHousekeeperCiAlreadyExist: Specification<HeadHousekeeper>
{
    public HeadHousekeeperCiAlreadyExist(string ci)
    {
        Query.Where(x => x.CI == ci);
    }
}