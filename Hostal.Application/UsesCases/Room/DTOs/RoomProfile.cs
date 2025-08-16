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
        
        CreateMap<Domain.Entities.Reservation, RoomReservationActiveDto.RoomReservationActiveDto>()
            .ForMember(x => x.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(x => x.ClientName, opt => opt.MapFrom(src => src.Client.Name));
        
        CreateMap<Domain.Entities.Reservation, RoomReservationsCanceledDto.RoomReservationsCanceledDto>()
            .ForMember(x => x.CancellationDate, opt => opt.MapFrom(src => src.CancellationDate))
            .ForMember(x => x.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(x => x.ClientName, opt => opt.MapFrom(src => src.Client.Name));

        CreateMap<Domain.Entities.Reservation, RoomReservationPastDto.RoomReservationPastDto>()
            .ForMember(x => x.ClientId, opt => opt.MapFrom(src => src.ClientId))
            .ForMember(x => x.ClientName, opt => opt.MapFrom(src => src.Client.Name));;

        CreateMap<Domain.Entities.RoomHeadHousekeeper, RoomHeadHousekeepersDto.RoomHeadHousekeepersDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HeadHousekeeper.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.HeadHousekeeper.Name))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.HeadHousekeeper.LastName));

        CreateMap<Domain.Entities.Room, AvailableRoomDto.AvailableRoomDto>()
            .ForMember(x => x.IsOutOfService, opt => opt.MapFrom(src => src.IsOutOfService))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(x => x.Capacity, opt => opt.MapFrom(src => src.Capacity))
            .ForMember(x => x.Number, opt => opt.MapFrom(src => src.Number));
    }
}