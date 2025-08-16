using Hostal.Application.UsesCases.Room.DTOs.AvailableRoomDto;
using MediatR;

namespace Hostal.Application.UsesCases.Room.Queries.GetAvailableRoomsQuery;

public class GetAvailableRoomsQuery : IRequest<List<AvailableRoomDto>>
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}