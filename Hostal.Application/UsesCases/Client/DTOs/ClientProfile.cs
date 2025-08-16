using AutoMapper;
using Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientQueryDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationCanceledDto;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto.ClientReservationDto;

namespace Hostal.Application.UsesCases.Client.DTOs;

public class ClientProfile: Profile
{
    public ClientProfile()
    {
        CreateMap<Domain.Entities.Client, ClientQueryDto>();
        CreateMap<CreateClientCommand, Domain.Entities.Client>();
        CreateMap<UpdateClientCommand, Domain.Entities.Client>();
        CreateMap<Domain.Entities.Reservation, ClientReservationDto>()
            .ForMember(x => x.RoomId, opt => opt.MapFrom(a => a.RoomId))
            .ForMember(x => x.RoomNumber, opt => opt.MapFrom(a => a.Room.Number));
        CreateMap<Domain.Entities.Reservation, ClientReservationCanceledDto>()
            .ForMember(x => x.RoomId, opt => opt.MapFrom(a => a.RoomId))
            .ForMember(x => x.RoomNumber, opt => opt.MapFrom(a => a.Room.Number));
    }
}