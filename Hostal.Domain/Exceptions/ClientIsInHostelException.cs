namespace Hostal.Domain.Exceptions;

public class ClientIsInHostelException()
    : Exception("El cliente ya se encuentra en el hostal, no puede ser modificada su reservación");
