using FluentValidation;
using FluentValidation.AspNetCore;
using Hostal.Application.UsesCases.User;
using Microsoft.Extensions.DependencyInjection;

namespace Hostal.Application.ServicesExtensions;

/// <summary>
/// Extensions for IServiceCollection to configure application services.
/// </summary>
public static class ServicesCollectionExtensions
{
    /// <summary>
    /// Configures and registers application services in the dependency injection container.
    /// This method sets up MediatR for CQRS, AutoMapper for object mapping, FluentValidation
    /// for request validation, and HTTP context accessor for accessing the current HTTP context.
    /// </summary>
    /// <param name="serviceCollection">The service collection to configure.</param>
    /// <remarks>
    /// This extension method performs the following registrations:
    /// <list type="bullet">
    ///   <item>Registers all MediatR handlers from the current assembly</item>
    ///   <item>Configures AutoMapper with all profile classes from the current assembly</item>
    ///   <item>Registers all validators from the current assembly and enables automatic validation</item>
    ///   <item>Adds HTTP context accessor for accessing the current HTTP context in services</item>
    /// </list>
    /// </remarks>
    /// <example>
    /// <code>
    /// // In Startup.cs or Program.cs
    /// services.AddApplicationServices();
    /// </code>
    /// </example>
    public static void AddApplicationServices(this IServiceCollection serviceCollection)
    {
        var assembly = typeof(ServicesCollectionExtensions).Assembly;
        serviceCollection.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        serviceCollection.AddAutoMapper(assembly);
        serviceCollection.AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation();
        serviceCollection.AddHttpContextAccessor();
        serviceCollection.AddScoped<IUserContext, UserContext>();
    }
}