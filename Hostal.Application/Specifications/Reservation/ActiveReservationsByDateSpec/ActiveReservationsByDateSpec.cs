using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.ActiveReservationsByDateSpec;

// <summary>
/// Specification para obtener reservas activas en una fecha específica
/// </summary>
public class ActiveReservationsByDateSpec : Specification<Domain.Entities.Reservation>
{
    public ActiveReservationsByDateSpec(DateTime date)
    {
        Query
            .Where(r => !r.IsCanceled && // No canceladas
                        r.StartDateReservation.Date <= date.Date && // Fecha de inicio menor o igual a la fecha consultada
                        r.EndDateReservation.Date >= date.Date) // Fecha de fin mayor o igual a la fecha consultada
            .Include(r => r.Client)
            .Include(r => r.Room)
            .OrderBy(r => r.Room.Number)
            .ThenBy(r => r.Client.Name);
    }
}