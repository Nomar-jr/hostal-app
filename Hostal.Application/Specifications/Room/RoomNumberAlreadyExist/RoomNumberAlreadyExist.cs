using Ardalis.Specification;

namespace Hostal.Application.Specifications.Room.RoomNumberAlreadyExist;

public sealed class RoomNumberAlreadyExist: Specification<Domain.Entities.Room>
{
    public RoomNumberAlreadyExist(string number)
    {
        Query.Where(room => room.Number == number);
    }
}