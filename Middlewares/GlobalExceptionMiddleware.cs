using System.Net;
using System.Text.Json;
using FluentValidation;
using MongoDB.Driver;
using ecom_api_nosql_.Common.Exceptions;

namespace ecom_api_nosql_.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteValidationError(context, ex);
        }
        catch (NotFoundException ex)
        {
            await WriteError(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (BadRequestException ex)
        {
            await WriteError(context, HttpStatusCode.BadRequest, ex.Message);
        }
        catch (MongoWriteException ex) when (ex.WriteError?.Category == ServerErrorCategory.DuplicateKey)
        {
            await WriteError(context, HttpStatusCode.Conflict, "Duplicate key: une valeur doit être unique (ex: email).");
        }
        catch (FormatException)
        {
            await WriteError(context, HttpStatusCode.BadRequest, "Format invalide.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");
            await WriteError(context, HttpStatusCode.InternalServerError, "Unexpected error.");
        }
    }

    private static async Task WriteError(HttpContext ctx, HttpStatusCode code, string message)
    {
        ctx.Response.StatusCode = (int)code;
        ctx.Response.ContentType = "application/json";

        var payload = new
        {
            traceId = ctx.TraceIdentifier,
            statusCode = (int)code,
            message
        };

        await ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }

    private static async Task WriteValidationError(HttpContext ctx, ValidationException ex)
    {
        ctx.Response.StatusCode = StatusCodes.Status400BadRequest;
        ctx.Response.ContentType = "application/json";

        var errors = ex.Errors
            .GroupBy(e => e.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ErrorMessage).ToArray());

        var payload = new
        {
            traceId = ctx.TraceIdentifier,
            statusCode = 400,
            message = "Validation failed.",
            errors
        };

        await ctx.Response.WriteAsync(JsonSerializer.Serialize(payload));
    }
}
