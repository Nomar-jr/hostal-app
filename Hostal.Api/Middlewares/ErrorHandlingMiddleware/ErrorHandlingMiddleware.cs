using FluentValidation;
using Hostal.Domain.Exceptions;

namespace Hostal.Api.Middlewares.ErrorHandlingMiddleware;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger): IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (ValidationException validationException)
        {
            context.Response.StatusCode = 400; // Bad Request
            context.Response.ContentType = "application/json";
            
            // Si es una ValidationException de FluentValidation
            if (validationException.Errors?.Any() == true)
            {
                var errors = validationException.Errors.Select(error => new
                {
                    PropertyName = error.PropertyName,
                    ErrorMessage = error.ErrorMessage,
                    ErrorCode = error.ErrorCode
                });

                var response = new
                {
                    Message = "Validation failed",
                    Errors = errors
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
            else
            {
                // ValidationException simple con solo mensaje
                var response = new
                {
                    Message = validationException.Message
                };

                await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
            }
        }
        catch (NotFoundException notFoundException)
        {
            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";
            
            var response = new
            {
                Message = notFoundException.Message
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        catch (ForbidException)
        {
            context.Response.StatusCode = 403;
            context.Response.ContentType = "application/json";
            
            var response = new
            {
                Message = "Insufficient permissions"
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
        catch (Exception e)
        {
            logger.LogError(e, e.Message);
            
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            
            var response = new
            {
                Message = e.Message
            };

            await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
        }
    }
}