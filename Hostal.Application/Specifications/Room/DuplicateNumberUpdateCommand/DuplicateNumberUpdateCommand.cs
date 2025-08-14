using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.DuplicateNumberUpdateCommand;

public sealed class DuplicateNumberUpdateCommand: Specification<Domain.Entities.Room>
{
    public DuplicateNumberUpdateCommand(string roomNumber, int id)
    {
        Query.Where(x => x.Number == roomNumber && x.Id != id);
    }
}