namespace Hostal.Domain.Exceptions;

public class OverlappingReservationsException() : Exception(
    "El cliente ya tiene una reserva activa en el período seleccionado. No se pueden tener reservas superpuestas.");