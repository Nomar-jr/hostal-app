using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.HeadHousekeeperCiAlreadyExist;
using Hostal.Application.Specifications.Room.ExistingRoomForHeadHousekeeper;
using Hostal.Domain.Entities;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;

public class CreateHeadHouseKeeperCommandHandle(
    ILogger<CreateHeadHouseKeeperCommandHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repositoryHeadHousekeeper,
    IRepository<Domain.Entities.Room> repositoryRoom,
    IRepository<Domain.Entities.RoomHeadHousekeeper> repositoryRoomHeadHousekeeper,
    IMapper mapper) : IRequestHandler<CreateHeadHouseKeeperCommand, int>
{
    public async Task<int> Handle(CreateHeadHouseKeeperCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new headhousekeeper with CI: {CI}", request.CI);
        
        // 1. Validar que no exista otro HeadHousekeeper con el mismo CI
        var existingHeadHousekeeper = await repositoryHeadHousekeeper.FirstOrDefaultAsync(
            new HeadHousekeeperCiAlreadyExist(request.CI),
            cancellationToken);
        if (existingHeadHousekeeper != null)
        {
            throw new InvalidOperationException($"La ama de llaves con CI: {request.CI} ya existe");
        }
        
        // 2. Validar que las rooms existan y estén activas
        if (request.RoomIds.Any())
        {
            var existingRoom = await repositoryRoom.ListAsync(new ExistingRoomForHeadHousekeeper(request.RoomIds), cancellationToken);
            var existingRoomIds = existingRoom.Select(x => x.Id).ToList(); 
            var nonExistingRoomIds = request.RoomIds.Except(existingRoomIds).ToList();
            if (nonExistingRoomIds.Any())
            {
                throw new ArgumentException(
                    $"Las siguientes habitaciones no existen o estan inactivas: {string.Join(", ", nonExistingRoomIds)}");
            }
        }
        // 3. Crear y mapear el HeadHousekeeper
        var headHouseKeeper = mapper.Map<Domain.Entities.HeadHousekeeper>(request);
        
        // 4. Guardar el HeadHousekeeper
        var createdHeadHousekeeper = await repositoryHeadHousekeeper.AddAsync(headHouseKeeper, cancellationToken);
        
        // 5. Crear las relaciones intermedias si hay Rooms asignadas
        if (request.RoomIds.Any())
        {
            var roomHeadHousekeepers = request.RoomIds.Select(x => new RoomHeadHousekeeper()
            {
                RoomId = x,
                HeadHousekeeperId = createdHeadHousekeeper.Id,
                AssignedDate = DateTime.UtcNow
            });
            // Guardar las relaciones
            foreach (var relationship in roomHeadHousekeepers)
            {
                await repositoryRoomHeadHousekeeper.AddAsync(relationship, cancellationToken);
            }

            logger.LogInformation(
                $"Created {roomHeadHousekeepers.Count()} room assignments for HeadHousekeeper {createdHeadHousekeeper.Id}");
        }
        return createdHeadHousekeeper.Id;
    }
}