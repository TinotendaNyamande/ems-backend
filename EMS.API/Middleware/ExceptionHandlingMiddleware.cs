using EMS.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Projects.Domain.Exceptions;
using System.Diagnostics;

namespace EMS.API.Middleware
{
    public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> _logger, IWebHostEnvironment _environment) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ValidationException validationException)
            {
                _logger.LogWarning(validationException, "Validation failed");

                var errors = validationException.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).ToArray()
                    );

                await WriteProblemDetailsAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    "Validation failed",
                    "One or more validation errors occurred.",
                    errors
                );
            }
            catch (ResourceNotFoundException ex)
            {
                _logger.LogWarning(ex, "Warning:Resource not found");
                await WriteProblemDetailsAsync(context, StatusCodes.Status404NotFound, "Resource Not Found", ex.Message);
            }
            catch (UnauthorizedAccessException unauthorized)
            {
                _logger.LogWarning(unauthorized, "Unauthorized access");
                await WriteProblemDetailsAsync(
                    context,
                    StatusCodes.Status403Forbidden,
                    "You do not have permission to perform this action.",
                    _environment.IsDevelopment() ? unauthorized.Message : null
                );
            }
            catch (ArgumentException badRequest)
            {
                _logger.LogWarning(badRequest, "Bad request");
                await WriteProblemDetailsAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    "Invalid request.",
                    _environment.IsDevelopment() ? badRequest.Message : null
                );
            }
            catch (InvalidOperationException invalidOperation)
            {
                _logger.LogWarning(invalidOperation, "Invalid operation");
                await WriteProblemDetailsAsync(
                    context,
                    StatusCodes.Status400BadRequest,
                    "Invalid operation.",
                    _environment.IsDevelopment() ? invalidOperation.Message : null
                );
            }
            catch (BusinessRuleException ex)
            {
                _logger.LogWarning(ex, "Business rule violation");

                await WriteProblemDetailsAsync(
                    context,
                    StatusCodes.Status409Conflict,
                    "Business rule violation",
                    ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                await WriteProblemDetailsAsync(context, StatusCodes.Status500InternalServerError,
                    "An unexpected error occurred", _environment.IsDevelopment() ? ex.Message : "Please contact support if the problem persists.");
            }
        }
        private async Task WriteProblemDetailsAsync(HttpContext context, int statusCode, string title, string? detail, IDictionary<string, string[]>? errors = null)
        {
            if (context.Response.HasStarted)
            {
                return;
            }

            var problem = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = detail,
                Instance = context.Request.Path
            };

            problem.Extensions["traceId"] = Activity.Current?.Id ?? context.TraceIdentifier;
            if (errors is not null)
            {
                problem.Extensions["errors"] = errors;
            }

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}
