using AutoMapper;
using Hostal.Application.UsesCases.HeadHousekeeper.DTOs.QueriesDto;

namespace Hostal.Application.UsesCases.HeadHousekeeper.DTOs;

public class HeadHouseKeeperProfile: Profile
{
    public HeadHouseKeeperProfile()
    {
        CreateMap<Domain.Entities.HeadHousekeeper, HeadHouseKeeperQueryDto>();
    }
}