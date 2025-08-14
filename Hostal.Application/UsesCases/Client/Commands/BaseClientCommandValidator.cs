using FluentValidation;
using Hostal.Application.UsesCases.Client.DTOs.CommandsDto;

namespace Hostal.Application.UsesCases.Client.Commands;

public class BaseClientCommandValidator: AbstractValidator<ClientCommandDto>
{
    public BaseClientCommandValidator()
    {
        // Validación del CI (Carnet de Identidad)
        RuleFor(x => x.CI)
            .NotEmpty()
            .WithMessage("Ingrese el carnet de identidad de la ama de llaves.")
            .Length(11)
            .WithMessage("El carnet de identidad debe tener exactamente 11 dígitos.")
            .Matches(@"^\d{11}$")
            .WithMessage("El carnet de identidad debe contener solo números.")
            .Must(BeValidCiNumber)
            .WithMessage("El carnet de identidad no tiene un formato válido.");

        // Validación del Nombre
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Ingrese el nombre del cliente.")
            .Length(2, 100)
            .WithMessage("El nombre debe tener entre 2 y 100 caracteres.")
            .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$")
            .WithMessage("El nombre solo puede contener letras y espacios.")
            .Must(NotBeOnlySpaces)
            .WithMessage("El nombre no puede estar compuesto solo de espacios.");

        // Validación de los Apellidos
        RuleFor(x => x.LastName)
            .NotEmpty()
            .WithMessage("Ingrese los apellidos del cliente.")
            .Length(2, 100)
            .WithMessage("Los apellidos deben tener entre 2 y 100 caracteres.")
            .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$")
            .WithMessage("Los apellidos solo pueden contener letras y espacios.")
            .Must(NotBeOnlySpaces)
            .WithMessage("Los apellidos no pueden estar compuestos solo de espacios.");

        // Validación del Teléfono
        RuleFor(x => x.Phone)
            .NotEmpty()
            .WithMessage("Ingrese el número de teléfono del cliente.")
            .Length(7, 20)
            .WithMessage("El número de teléfono debe tener entre 7 y 20 caracteres.")
            .Matches(@"^[\d\+\-\(\)\s]+$")
            .WithMessage("El número de teléfono solo puede contener números, +, -, paréntesis y espacios.")
            .Must(BeValidPhoneNumber)
            .WithMessage("El formato del número de teléfono no es válido.");
    }

    /// <summary>
    /// Valida que el CI tenga exactamente 11 dígitos numéricos
    /// </summary>
    private static bool BeValidCiNumber(string ci)
    {
        if (string.IsNullOrWhiteSpace(ci))
            return false;

        // Verificar que tenga exactamente 11 caracteres y todos sean dígitos
        return ci.Length == 11 && ci.All(char.IsDigit);
    }

    /// <summary>
    /// Valida que el texto no esté compuesto solo de espacios
    /// </summary>
    private static bool NotBeOnlySpaces(string text)
    {
        return !string.IsNullOrWhiteSpace(text) && text.Trim().Length > 0;
    }

    /// <summary>
    /// Valida el formato básico del número de teléfono
    /// </summary>
    private static bool BeValidPhoneNumber(string phone)
    {
        if (string.IsNullOrWhiteSpace(phone))
            return false;

        // Remover espacios y caracteres especiales para contar solo dígitos
        var digitsOnly = new string(phone.Where(char.IsDigit).ToArray());
        
        // Debe tener al menos 7 dígitos (mínimo para un teléfono válido)
        if (digitsOnly.Length < 7)
            return false;

        // Verificar que no empiece con 0 (opcional)
        // return !digitsOnly.StartsWith("0");
        
        return true;
    }
}