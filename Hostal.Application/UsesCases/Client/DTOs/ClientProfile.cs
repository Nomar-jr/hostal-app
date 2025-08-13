using AutoMapper;
using Hostal.Application.UsesCases.Client.Commands.CreateClientCommand;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.Client.DTOs.QueriesDto;

namespace Hostal.Application.UsesCases.Client.DTOs;

public class ClientProfile: Profile
{
    public ClientProfile()
    {
        CreateMap<Domain.Entities.Client, ClientQueryDto>();
        CreateMap<CreateClientCommand, Domain.Entities.Client>();
        CreateMap<UpdateClientCommand, Domain.Entities.Client>();
    }
}