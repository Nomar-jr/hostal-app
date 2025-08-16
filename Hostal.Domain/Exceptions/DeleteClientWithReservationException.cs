namespace Hostal.Domain.Exceptions;

public class DeleteClientWithReservationException()
    : Exception("Este cliente no puede ser eliminado porque tiene reservas");
