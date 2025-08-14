using AutoMapper;
using Hostal.Application.Specifications.HeadHouseKeeper.DuplicateCiUpdateCommand;
using Hostal.Application.Specifications.Room.ExistingRoomForHeadHousekeeper;
using Hostal.Application.Specifications.RoomHeadHousekeeper;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.CommandsDto;
using Hostal.Domain.Entities;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;

public class UpdateHeadHouseKeeperCommandHandle(
    ILogger<CreateHeadHouseKeeperCommandHandle> logger,
    IRepository<Domain.Entities.HeadHousekeeper> repositoryHeadHousekeeper,
    IRepository<Domain.Entities.Room> repositoryRoom,
    IRepository<Domain.Entities.RoomHeadHousekeeper> repositoryRoomHeadHousekeeper,
    IMapper mapper): IRequestHandler<UpdateHeadHouseKeeperCommand>
{
    public async Task Handle(UpdateHeadHouseKeeperCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Updating headhousekeeper with identifier {request.Id}");
        
        // 1. Obtener el HeadHousekeeper existente
        var existingHeadHousekeeper = await repositoryHeadHousekeeper.GetByIdAsync(request.Id, cancellationToken) ??
                                      throw new NotFoundException(nameof(Domain.Entities.HeadHousekeeper),
                                          request.Id.ToString());
        
        // 2. Validar CI único (excluyendo CI de HeadHousekeeper actual)
        var duplicateCi =
            await repositoryHeadHousekeeper.FirstOrDefaultAsync(new DuplicateCiUpdateCommand(request.CI, request.Id),
                cancellationToken);
        if (duplicateCi != null)
        {
            throw new InvalidOperationException($"Another HeadHousekeeper with CI '{request.CI}' already exists.");
        }
        
        // 3. Validad Rooms si se proporcionaron
        if (request.RoomIds.Any())
        {
            var existingRoom = await repositoryRoom.ListAsync(new ExistingRoomForHeadHousekeeper(request.RoomIds),
                cancellationToken);
            var existingRoomIds = existingRoom.Select(x => x.Id).ToList();
            var nonExistingRoomIds = request.RoomIds.Except(existingRoomIds).ToList();
            if (nonExistingRoomIds.Any())
            {
                throw new ArgumentException(
                    $"Las siguientes habitaciones no existen o estan inactivas: {string.Join(", ", nonExistingRoomIds)}");
            }
        }
        
        // 4. Actializar las propiedades del HeadHousekeeper
        mapper.Map(request, existingHeadHousekeeper);
        await repositoryHeadHousekeeper.UpdateAsync(existingHeadHousekeeper, cancellationToken);
        
        // 5. Actualizar las relaciones con Rooms
        // Primero se obtienen las relaciones actuales
        var currentRelationship =
            await repositoryRoomHeadHousekeeper.ListAsync(new RoomHeadHousekeepersByHeadHousekeeperId(request.Id),
                cancellationToken);
        
        // Eliminar relaciones que ya no están en la nueva lista
        var currentRoomIds = currentRelationship.Select(x => x.RoomId).ToList();
        var relationshipsToRemove = currentRelationship
            .Where(x => !request.RoomIds.Contains(x.RoomId)).ToList();
        foreach (var relationship in relationshipsToRemove)
        {
            await repositoryRoomHeadHousekeeper.DeleteAsync(relationship, cancellationToken);
        }
        
        // Agregar las nuevas relaciones
        var newRoomIds = request.RoomIds.Except(currentRoomIds).ToList();
        var newRelationship = newRoomIds.Select(x => new RoomHeadHousekeeper()
        {
            RoomId = x,
            HeadHousekeeperId = request.Id,
            AssignedDate = DateTime.UtcNow
        });
        foreach (var relationship in newRelationship)
        {
            await repositoryRoomHeadHousekeeper.AddAsync(relationship, cancellationToken);
        }
        logger.LogInformation("HeadHousekeeper {Id} updated successfully. Removed {RemovedCount} room assignments, added {AddedCount} new assignments", 
            request.Id, relationshipsToRemove.Count, newRelationship.Count());
    }
}