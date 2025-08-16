using Ardalis.Specification;

namespace Hostal.Application.Specifications.Reservation.ClientOverlappingReservationsSpec;

// Especificación para buscar reservas superpuestas de un cliente
public class ClientOverlappingReservationsSpec : Specification<Domain.Entities.Reservation>
{
    public ClientOverlappingReservationsSpec(int clientId, DateTime startDate, DateTime endDate, int? excludeReservationId = null)
    {
        Query.Where(r => r.ClientId == clientId 
                         && !r.IsCanceled  // Solo considerar reservas activas
                         && (
                             // Caso 1: La nueva reserva empieza durante una reserva existente
                             (startDate >= r.StartDateReservation && startDate < r.EndDateReservation) ||
                             // Caso 2: La nueva reserva termina durante una reserva existente
                             (endDate > r.StartDateReservation && endDate <= r.EndDateReservation) ||
                             // Caso 3: La nueva reserva engloba completamente una reserva existente
                             (startDate <= r.StartDateReservation && endDate >= r.EndDateReservation) ||
                             // Caso 4: Una reserva existente engloba completamente la nueva reserva
                             (r.StartDateReservation <= startDate && r.EndDateReservation >= endDate)
                         ));

        // Si estamos actualizando una reserva existente, excluirla de la búsqueda
        if (excludeReservationId.HasValue)
        {
            Query.Where(r => r.Id != excludeReservationId.Value);
        }
    }
}