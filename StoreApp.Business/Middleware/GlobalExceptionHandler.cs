using System.Text.Json;
using Microsoft.AspNetCore.Http;
using StoreApp.Business.Exceptions;

namespace StoreApp.Business.Middleware;

public class GlobalExceptionHandler : IMiddleware
{

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "application/json";
            var response = new ErrorResponse();
            switch(ex)
            {
                case ValidationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    response.Message = ex.Message;
                    break;
                
                case NotFoundException:
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    response.Message = ex.Message;
                    break;
                
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    response.Message = "Internal Server Error";
                    break;
            }
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

public class ErrorResponse
{
    public string? Message { get; set; }
}