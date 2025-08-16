using AutoMapper;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;
using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.HeadHousekeeper.DTOs;

public class HeadHouseKeeperProfile: Profile
{
    public HeadHouseKeeperProfile()
    {
        CreateMap<Domain.Entities.HeadHousekeeper, HeadHouseKeeperQueryDto>();
        
        CreateMap<CreateHeadHouseKeeperCommand, Domain.Entities.HeadHousekeeper>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.RoomHeadHousekeepers, opt => opt.Ignore());
        
        CreateMap<UpdateHeadHouseKeeperCommand, Domain.Entities.HeadHousekeeper>()
            .ForMember(x => x.RoomHeadHousekeepers, opt => opt.Ignore());

        CreateMap<RoomHeadHousekeeper, HeadHousekeeperRoomDto.HeadHousekeeperRoomDto>()
            .ForMember(x => x.Id, opt => opt.MapFrom(a => a.RoomId))
            .ForMember(x => x.Number, opt => opt.MapFrom(a => a.Room.Number));
    }
}