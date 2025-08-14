using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.ExistingHeadHousekeeperforRoom;
using Hostal.Application.Specifications.Room.ExistingRoomForHeadHousekeeper;
using Hostal.Application.Specifications.Room.RoomNumberAlreadyExist;
using Hostal.Domain.Entities;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;

public class CreateRoomCommandHandle(
    ILogger<CreateRoomCommandHandle> logger,
    IRepository<Domain.Entities.Room> repositoryRoom,
    IRepository<Domain.Entities.HeadHousekeeper> repositoryHeadHousekeeper,
    IRepository<RoomHeadHousekeeper> repositoryRoomHeadHousekeeper,
    IMapper mapper) : IRequestHandler<CreateRoomCommand, int>
{
    public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new room with Number: {number}", request.Number);
        
        // 1. Validar que no exista otra habitación con el mismo número
        var existingRoom = await repositoryRoom.FirstOrDefaultAsync(
            new RoomNumberAlreadyExist(request.Number),
            cancellationToken);
        if (existingRoom != null)
        {
            throw new InvalidOperationException($"La habitación con el número: {request.Number} ya existe");
        }
        
        // 2. Validar que las amas de llaves existan y estén activas
        if (request.HeadHousekeeperIds.Count != 0)
        {
            var existingHeadHousekeeper = await repositoryHeadHousekeeper.ListAsync(new ExistingHeadHousekeeperforRoom(request.HeadHousekeeperIds), cancellationToken);
            var existingHeadHousekeeperIds = existingHeadHousekeeper.Select(x => x.Id).ToList(); 
            var nonExistingHeadHousekeeperIds = request.HeadHousekeeperIds.Except(existingHeadHousekeeperIds).ToList();
            if (nonExistingHeadHousekeeperIds.Count != 0)
            {
                throw new ArgumentException(
                    $"Las siguientes amas de llaves no existen o están inactivas: {string.Join(", ", nonExistingHeadHousekeeperIds)}");
            }
        }
        // 3. Crear y mapear el HeadHousekeeper
        var room = mapper.Map<Domain.Entities.Room>(request);
        
        // 4. Guardar el HeadHousekeeper
        var createdRoom = await repositoryRoom.AddAsync(room, cancellationToken);
        
        // 5. Crear las relaciones intermedias si hay Rooms asignadas
        if (request.HeadHousekeeperIds.Count == 0) return createdRoom.Id;
        
        var headHousekeeperRoom = request.HeadHousekeeperIds.Select(x => new RoomHeadHousekeeper()
        {
            HeadHousekeeperId= x,
            RoomId = createdRoom.Id,
            AssignedDate = DateTime.UtcNow
        });
        // Guardar las relaciones
        foreach (var relationship in headHousekeeperRoom)
        {
            await repositoryRoomHeadHousekeeper.AddAsync(relationship, cancellationToken);
        }

        logger.LogInformation(
            $"Created {headHousekeeperRoom.Count()} room assignments for HeadHousekeeper {createdRoom.Id}");
        
        return createdRoom.Id;
    }
}