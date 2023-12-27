using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Okai.Boilerplate.Domain.Exceptions;
using Okai.Boilerplate.Domain.Mediator.Validation;
using System.Net;
using System.Text.Json;

namespace Okai.Boilerplate.Domain.Middlewares
{

    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IHttpContextAccessor _accessor;

        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, IHttpContextAccessor accessor, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _accessor = accessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (CustomCommandException ex)
            {
                _logger.LogError(ex, $"An error has ocurred during the process. Trace Identifier: {_accessor.HttpContext?.TraceIdentifier}.");

                if (_accessor.HttpContext != null)
                    await handleExceptionMessageAsync(_accessor.HttpContext, ex.Message, ex.ValidationFailures).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "An error has ocurred during the process. Trace Identifier: " + _accessor.HttpContext?.TraceIdentifier + ".");
                if (_accessor.HttpContext != null)
                {
                    await handleExceptionMessageAsync(_accessor.HttpContext).ConfigureAwait(continueOnCapturedContext: false);
                }
            }
        }

        private static Task handleExceptionMessageAsync(HttpContext context)
        {
            string text = JsonSerializer.Serialize(new ValidationProblemDetails
            {
                Title = "An error has occurred.",
                Status = 500,
                Instance = context.Request.Path
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(text);
        }
        private static Task handleExceptionMessageAsync(HttpContext context, string message, IList<ValidationFailure> validationFailures)
        {
            var response = new ValidationProblemDetails
            {
                Title = message,
                Status = (int)HttpStatusCode.InternalServerError,
                Instance = context.Request.Path
            };

            foreach (var validationFailure in validationFailures)
                response.Errors.Add(validationFailure.PropertyName, validationFailure.Errors.ToArray());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
