namespace Hostal.Application.UsesCases.Reservation.DTOs.QueriesDto.ActiveReservationDto;

/// <summary>
/// DTO que representa una reserva activa con información del cliente y habitación
/// </summary>
public class ActiveReservationDto
{
    public string ClientName { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsClientInHostel { get; set; }
}