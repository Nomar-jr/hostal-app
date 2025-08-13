using MediatR;

namespace Hostal.Application.UsesCases.Client.Commands.DeleteClientCommand;

public class DeleteClientCommand(int id): IRequest
{
    public int Id { get; } = id;
}