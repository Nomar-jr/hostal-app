using AutoMapper;
using Hostal.Application.UsesCases.Room.DTOs.RoomQueryDto;
using Hostal.Domain.Exceptions;
using Hostal.Domain.Interfaces;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetRoomById;

public record class GetRoomByIdQuery() : IRequest<RoomQueryDto>
{
    public int Id { get; set; }
}

public class GetRoomByIdHandle(IRepository<Domain.Entities.Room> repository, IMapper mapper): IRequestHandler<GetRoomByIdQuery, RoomQueryDto>
{
    public async Task<RoomQueryDto> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken) =>
    mapper.Map<RoomQueryDto>(await repository.FirstOrDefaultAsync(
            new Specifications.Room.GetRoomById.GetRoomById(request.Id),
            cancellationToken) ?? throw new NotFoundException(nameof(Domain.Entities.Room)
            , request.Id.ToString()));
    
}