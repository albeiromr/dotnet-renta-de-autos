using Application.Commons.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares;

internal record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors
    );

// este es un middleware personalizado para capturar todas las excepciones de la aplicación
// y retornarlas mediante http al usuario como respuesta, se explica en el video 56 del curso
// de udemy (midleware global en clean architechture
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate? _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware>? _logger;

    public GlobalExceptionHandlingMiddleware(
        RequestDelegate? next,
        ILogger<GlobalExceptionHandlingMiddleware>? logger
    )
    {
        _next = next;
        _logger = logger;
    }

    // evalua si alguna excepción ocurre en el programa
    // durante el ciclo de vida de la petición
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next!(httpContext);
        }
        catch (Exception ex)
        {
            _logger!.LogError(ex, $"There was an exception: {ex.Message}");

            var exceptionDetails = GetExceptionDetails(ex);
            ProblemDetails problemDetails = new ProblemDetails
            {
                Status = exceptionDetails.Status,
                Type = exceptionDetails.Type,
                Title = exceptionDetails.Title,
                Detail = exceptionDetails.Detail,
            };

            if( exceptionDetails.Errors != null )
            {
                problemDetails.Extensions["errors"] = exceptionDetails.Errors;
            };

            httpContext.Response.StatusCode = exceptionDetails.Status;

            await httpContext.Response.WriteAsJsonAsync( problemDetails );
        }
    }

    private static ExceptionDetails GetExceptionDetails(Exception ex)
    {
        return ex switch
        {
            // si la excepción es de validación de data
            DataValidationException dataValidationExeption => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "Validation Failure",
                "Validation Error",
                "There where one or mor validation error",
                dataValidationExeption.Errors
            ),

            // si la excepción es de cualquier otro tipo
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "Server Error",
                "Error de Servidor",
                "an unexpected error happened into the application",
                null
            )
        };
    }
}
