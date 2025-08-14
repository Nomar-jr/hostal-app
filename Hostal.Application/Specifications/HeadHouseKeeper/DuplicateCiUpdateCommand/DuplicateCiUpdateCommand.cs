using Ardalis.Specification;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.HeadHouseKeeper.DuplicateCiUpdateCommand;

public sealed class DuplicateCiUpdateCommand: Specification<HeadHousekeeper>
{
    public DuplicateCiUpdateCommand(string ci, int id)
    {
        Query.Where(x => x.CI == ci && x.Id != id);
    }
}