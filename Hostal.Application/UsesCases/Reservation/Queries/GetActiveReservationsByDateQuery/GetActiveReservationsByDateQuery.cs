using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ActiveReservationDto;
using MediatR;

namespace Hostal.Application.UsesCases.Reservation.Queries.GetActiveReservationsByDateQuery;

/// <summary>
/// Query para obtener las reservas activas de un día específico
/// </summary>
public class GetActiveReservationsByDateQuery(DateTime date) : IRequest<IEnumerable<ActiveReservationDto>>
{
    public DateTime Date { get; set; } = date;
}