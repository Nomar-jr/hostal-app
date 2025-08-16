using Hostal.Domain.Constant;
using Hostal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace Hostal.Infrastructure.Seeders.RoomSeeders;

public class RoomSeeders(HostalDbContext dbContext): IRoomSeeders
{
    public async Task SeedAsync()
    {
        if (await dbContext.Database.CanConnectAsync())
        {
            if (!dbContext.Rooms.Any())
            {
                var rooms = GetRooms(); 
                await dbContext.Rooms.AddRangeAsync(rooms);
                await dbContext.SaveChangesAsync();
            }

            /*if (!dbContext.Roles.Any())
            {
                await dbContext.Roles.AddRangeAsync(GetIdentityRoles());
                await dbContext.SaveChangesAsync();
            }*/
        }
    }
    
    private IEnumerable<IdentityRole> GetIdentityRoles()
    {
        List<IdentityRole> roles =
        [
            new(UserRoles.Admin),
            new(UserRoles.User),
        ];
        return roles;
    }
    private IEnumerable<Domain.Entities.Room> GetRooms()
{
    List<Domain.Entities.Room> rooms = [
        // Piso 1
        new()
        {
            Number = "011",
            Capacity = 2,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "012",
            Capacity = 4,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "013",
            Capacity = 6,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "014",
            Capacity = 3,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "015",
            Capacity = 8,
            IsOutOfService = false,
            IsActive = true
        },
        // Piso 2
        new()
        {
            Number = "021",
            Capacity = 5,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "022",
            Capacity = 2,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "023",
            Capacity = 7,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "024",
            Capacity = 4,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "025",
            Capacity = 9,
            IsOutOfService = false,
            IsActive = true
        },
        // Piso 3
        new()
        {
            Number = "031",
            Capacity = 3,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "032",
            Capacity = 6,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "033",
            Capacity = 10,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "034",
            Capacity = 5,
            IsOutOfService = false,
            IsActive = true
        },
        new()
        {
            Number = "035",
            Capacity = 8,
            IsOutOfService = false,
            IsActive = true
        }
    ];
    return rooms;
}
}