using AutoMapper;
using Hostal.Application.UsesCases.Reservation.Commands.CreateReservationCommand;
using Hostal.Application.UsesCases.Reservation.Commands.UpdateReservationCommand;
using Hostal.Application.UsesCases.Reservation.DTOs.CommandsDto;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Domain.Entities;

namespace Hostal.Application.UsesCases.Reservation.DTOs;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        // Mapeo de Command DTO a entidad
        CreateMap<ReservationCommandDto, Domain.Entities.Reservation>();
        
        CreateMap<CreateReservationCommand, Domain.Entities.Reservation>();
        
        CreateMap<UpdateReservationCommand, Domain.Entities.Reservation>();
        // Mapeo de entidad a Query DTO
        CreateMap<Domain.Entities.Reservation, ReservationQueryDto>()
            .ForMember(p => p.ClientId, opt => opt.MapFrom(src => src.Client.Id))
            .ForMember(p => p.ClientName, opt => opt.MapFrom(src => src.Client.Name))
            .ForMember(p => p.ClientLastName, opt => opt.MapFrom(src => src.Client.LastName))
            .ForMember(p => p.ClientCi, opt => opt.MapFrom(src => src.Client.CI))
            .ForMember(p => p.ClientPhone, opt => opt.MapFrom(src => src.Client.Phone))
            .ForMember(p => p.ClientEmail, opt => opt.MapFrom(src => src.Client.Email))
            .ForMember(p => p.ClientIsVip, opt => opt.MapFrom(src => src.Client.IsActive))
            .ForMember(p => p.RoomId, opt => opt.MapFrom(src => src.Room.Id))
            .ForMember(p => p.RoomNumber, opt => opt.MapFrom(src => src.Room.Number))
            .ForMember(p => p.RoomCapacity, opt => opt.MapFrom(src => src.Room.Capacity));
    }
}
