namespace Hostal.Application.UsesCases.Room.DTOs.AvailableRoomDto;

public class AvailableRoomDto
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
    public int Capacity { get; set; }
    public bool IsOutOfService { get; set; }
}