using AutoMapper;
using Hostal.Application.UsesCases.Client.Commands.UpdateClientCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.CreateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.Commands.UpdateHeadHouseKeeperCommand;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;

namespace Hostal.Application.UsesCases.HeadHousekeeper.DTOs;

public class HeadHouseKeeperProfile: Profile
{
    public HeadHouseKeeperProfile()
    {
        CreateMap<Domain.Entities.HeadHousekeeper, HeadHouseKeeperQueryDto>();
        CreateMap<CreateHeadHouseKeeperCommand, Domain.Entities.HeadHousekeeper>();
        CreateMap<UpdateHeadHouseKeeperCommand, Domain.Entities.HeadHousekeeper>();
    }
}