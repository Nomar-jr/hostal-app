using FluentValidation;
using Hostal.Application.UsesCases.Room.DTOs.RoomCommandDto;

namespace Hostal.Application.UsesCases.Room.Commands;

public class BaseRoomCommandValidator : AbstractValidator<RoomCommandDto>
{
    public BaseRoomCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .WithMessage("El número de habitación es requerido.")
            .Length(3)
            .WithMessage("El número de habitación debe tener exactamente 3 dígitos.")
            .Matches(@"^0[1-3][1-5]$")
            .WithMessage("El número de habitación debe seguir el formato 0XY, donde X es el piso (1-3) e Y es el número de habitación (1-5).")
            .Must(BeValidRoomNumber)
            .WithMessage("El número de habitación no es válido según las especificaciones del hostal.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("La capacidad debe ser mayor a 0.")
            .LessThanOrEqualTo(10)
            .WithMessage("La capacidad no puede exceder 10 ocupantes.");
    }

    /// <summary>
    /// Valida que el número de habitación siga las reglas del hostal:
    /// - 15 habitaciones total
    /// - 3 pisos con 5 habitaciones cada uno
    /// - Formato: 0XY donde X = piso (1-3), Y = habitación (1-5)
    /// </summary>
    private static bool BeValidRoomNumber(string roomNumber)
    {
        if (string.IsNullOrEmpty(roomNumber) || roomNumber.Length != 3)
            return false;

        // Verificar que comience con '0'
        if (roomNumber[0] != '0')
            return false;

        // Extraer el número del piso (X) y número de habitación (Y)
        if (!int.TryParse(roomNumber[1].ToString(), out int floor) || 
            !int.TryParse(roomNumber[2].ToString(), out int roomInFloor))
            return false;

        // Validar rango del piso (1-3)
        if (floor < 1 || floor > 3)
            return false;

        // Validar rango de habitación por piso (1-5)
        if (roomInFloor < 1 || roomInFloor > 5)
            return false;

        return true;
    }
}

// Clase auxiliar para generar números de habitación válidos
public static class RoomNumberHelper
{
    /// <summary>
    /// Genera todos los números de habitación válidos para el hostal
    /// </summary>
    /// <returns>Lista de números de habitación válidos</returns>
    public static List<string> GetAllValidRoomNumbers()
    {
        var validRoomNumbers = new List<string>();
        
        for (int floor = 1; floor <= 3; floor++)
        {
            for (int room = 1; room <= 5; room++)
            {
                validRoomNumbers.Add($"0{floor}{room}");
            }
        }
        
        return validRoomNumbers;
        // Resultado: ["011", "012", "013", "014", "015", "021", "022", "023", "024", "025", "031", "032", "033", "034", "035"]
    }

    /// <summary>
    /// Extrae el número de piso del número de habitación
    /// </summary>
    /// <param name="roomNumber">Número de habitación (formato 0XY)</param>
    /// <returns>Número de piso o null si el formato es inválido</returns>
    public static int? GetFloorFromRoomNumber(string roomNumber)
    {
        if (string.IsNullOrEmpty(roomNumber) || roomNumber.Length != 3 || roomNumber[0] != '0')
            return null;

        if (int.TryParse(roomNumber[1].ToString(), out int floor) && floor >= 1 && floor <= 3)
            return floor;

        return null;
    }

    /// <summary>
    /// Extrae el número de habitación dentro del piso
    /// </summary>
    /// <param name="roomNumber">Número de habitación (formato 0XY)</param>
    /// <returns>Número de habitación en el piso o null si el formato es inválido</returns>
    public static int? GetRoomInFloorFromRoomNumber(string roomNumber)
    {
        if (string.IsNullOrEmpty(roomNumber) || roomNumber.Length != 3 || roomNumber[0] != '0')
            return null;

        if (int.TryParse(roomNumber[2].ToString(), out int roomInFloor) && roomInFloor >= 1 && roomInFloor <= 5)
            return roomInFloor;

        return null;
    }
}