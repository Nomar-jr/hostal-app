using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.ExistingHeadHousekeeperforRoom;
using Hostal.Application.Specifications.Room.DuplicateNumberUpdateCommand;
using Hostal.Application.Specifications.RoomHeadHousekeeper;
using Hostal.Domain.Entities;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;

public class UpdateRoomCommandHandler(
    ILogger<UpdateRoomCommandHandler> logger,
    IRepository<Domain.Entities.Room> repositoryRoom,
    IRepository<Domain.Entities.HeadHousekeeper> repositoryHeadHousekeeper,
    IRepository<Domain.Entities.RoomHeadHousekeeper> repositoryRoomHeadHousekeeper,
    IMapper mapper): IRequestHandler<UpdateRoomCommand>
{
    public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating room with identifier {request.Id}");
        
        // 1. Obtener la habitación existente
        var existingRoom = await repositoryRoom.GetByIdAsync(request.Id, cancellationToken) ??
                                      throw new NotFoundException(nameof(Domain.Entities.Room),
                                          request.Id.ToString());
        
        // 2. Validar number único (excluyendo number de la room actual)
        var duplicateNumber =
            await repositoryRoom.FirstOrDefaultAsync(new DuplicateNumberUpdateCommand(request.Number, request.Id),
                cancellationToken);
        if (duplicateNumber != null)
        {
            throw new InvalidOperationException($"Another Room with Number '{request.Number}' already exists.");
        }
        
        // 3. Validad HeadHousekeepers si se proporcionaron
        if (request.HeadHousekeeperIds.Any())
        {
            var existingHeadHousekeeper = await repositoryHeadHousekeeper.ListAsync(new ExistingHeadHousekeeperforRoom(request.HeadHousekeeperIds),
                cancellationToken);
            var existingHeadHousekeeperIds = existingHeadHousekeeper.Select(x => x.Id).ToList();
            var nonExistingHeadHousekeeperIds = request.HeadHousekeeperIds.Except(existingHeadHousekeeperIds).ToList();
            if (nonExistingHeadHousekeeperIds.Any())
            {
                throw new ArgumentException(
                    $"Las siguientes amas de llaves no existen o están inactivas: {string.Join(", ", nonExistingHeadHousekeeperIds)}");
            }
        }
        
        // 4. Actializar las propiedades de la Room
        mapper.Map(request, existingRoom);
        await repositoryRoom.UpdateAsync(existingRoom, cancellationToken);
        
        // 5. Actualizar las relaciones con HeadHousekeeper
        // Primero se obtienen las relaciones actuales
        var currentRelationship =
            await repositoryRoomHeadHousekeeper.ListAsync(new RoomHeadHousekeepersByRoomId(request.Id),
                cancellationToken);
        
        // Eliminar relaciones que ya no están en la nueva lista
        var currentHeadHousekeeperIds = currentRelationship.Select(x => x.HeadHousekeeperId).ToList();
        var relationshipsToRemove = currentRelationship
            .Where(x => !request.HeadHousekeeperIds.Contains(x.RoomId)).ToList();
        foreach (var relationship in relationshipsToRemove)
        {
            await repositoryRoomHeadHousekeeper.DeleteAsync(relationship, cancellationToken);
        }
        
        // Agregar las nuevas relaciones
        var newHeadHousekeeperIds = request.HeadHousekeeperIds.Except(currentHeadHousekeeperIds).ToList();
        var newRelationship = newHeadHousekeeperIds.Select(x => new RoomHeadHousekeeper()
        {
            HeadHousekeeperId= x,
            RoomId = request.Id,
            AssignedDate = DateTime.UtcNow
        });
        foreach (var relationship in newRelationship)
        {
            await repositoryRoomHeadHousekeeper.AddAsync(relationship, cancellationToken);
        }
        logger.LogInformation("Room {Id} updated successfully. Removed {RemovedCount} head housepeeker assignments, added {AddedCount} new assignments", 
            request.Id, relationshipsToRemove.Count, newRelationship.Count());
    }
}