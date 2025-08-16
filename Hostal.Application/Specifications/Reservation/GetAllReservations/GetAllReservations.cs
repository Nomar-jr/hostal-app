using Ardalis.Specification;
using Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto;
using Hostal.Domain.Entities;

namespace Hostal.Application.Specifications.Reservation.GetAllReservations;

public sealed class GetAllReservations : Specification<Domain.Entities.Reservation>
{
    public GetAllReservations(int pageNumber, int pageSize)
    {
        Query.Where(x => x.IsCanceled == false && x.StartDateReservation >= DateTime.Now)
             .Include(x => x.Client)
             .Include(r => r.Room);
        Query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .OrderBy(c => c.Id);
    }
}