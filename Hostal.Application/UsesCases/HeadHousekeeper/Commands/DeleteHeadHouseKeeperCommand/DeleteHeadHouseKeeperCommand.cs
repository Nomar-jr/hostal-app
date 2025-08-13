using MediatR;

namespace Hostal.Application.UsesCases.HeadHousekeeper.Commands.DeleteHeadHouseKeeperCommand;

public class DeleteHeadHouseKeeperCommand(int id): IRequest
{
    public int Id { get;} = id;
}