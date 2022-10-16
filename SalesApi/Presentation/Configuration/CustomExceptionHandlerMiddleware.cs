using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;
using System.Net.Mime;
using System.Text.Encodings.Web;
using System.Text.Json;
using Presentation.Models;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace Presentation.Configuration
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            int code;
            string result;

            var exceptionModel = new ExceptionModel(exception.Message);
            var options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            switch (exception)
            {
                case NotFoundException notFoundException:
                    code = StatusCodes.Status404NotFound;
                    exceptionModel.Detail = notFoundException.Detail;
                    result = JsonSerializer.Serialize(exceptionModel, options);
                    break;
                case AccessException accessException:
                    code = StatusCodes.Status403Forbidden;
                    exceptionModel.Detail = accessException.Detail;
                    result = JsonSerializer.Serialize(exceptionModel, options);
                    break;
                default:
                    code = StatusCodes.Status500InternalServerError;
                    result = JsonSerializer.Serialize(exceptionModel, options);
                    break;
            }

            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }

    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
