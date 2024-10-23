using FluentValidation;
using InteractiveDashboard.Domain.Constants;
using InteractiveDashboard.Domain.Dtos;
using InteractiveDashboard.Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace InteractiveDashboard.Api.Middleware
{
    public class ExceptionMidlleware
    {
        readonly RequestDelegate _next;

        public ExceptionMidlleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (GeneralException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new ErrorResponse(new[] { ex.Error }, ErrorCodes.GenrealError);
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
            catch (MultipleException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new ErrorResponse(ex.Errors, ErrorCodes.MultipleError);
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
            catch (ValidationException ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var error = new ErrorResponse(ex.Errors.Select(e => e.ErrorMessage), ErrorCodes.ValidationError);
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
            catch (Exception ex)
            {
                //Log error in a meaningful way
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new ErrorResponse(new[] { "Unknown Error" }, ErrorCodes.UnexpectedError);
                await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            }
        }
    }
}
