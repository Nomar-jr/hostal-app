using AutoMapper;
using Hostal.Application.UsesCases.Room.Commands.CreateRoomCommand;
using Hostal.Application.UsesCases.Room.Commands.UpdateRoomCommand;

namespace Hostal.Application.UsesCases.Room.DTOs;

public class RoomProfile: Profile
{
    public RoomProfile()
    {
        CreateMap<Domain.Entities.Room, RoomQueryDto.RoomQueryDto>();
        
        CreateMap<CreateRoomCommand, Domain.Entities.Room>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.RoomHeadHousekeepers, opt => opt.Ignore());
        
        CreateMap<UpdateRoomCommand, Domain.Entities.Room>()
            .ForMember(x => x.RoomHeadHousekeepers, opt => opt.Ignore());
    }
}