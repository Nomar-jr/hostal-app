namespace Hostal.Domain.Entities;

public class RoomHeadHousekeeper
{
    public int RoomId { get; set; }
    public int HeadHousekeeperId { get; set; }
    
    //Propiedades adicionales
    public DateTime AssignedDate { get; set; } = DateTime.UtcNow;
    
    public virtual Room Room { get; set; }
    public virtual HeadHousekeeper HeadHousekeeper { get; set; }
}