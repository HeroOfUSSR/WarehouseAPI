using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Domain.Exceptions;

namespace Warehouse.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception, CancellationToken ct)
        {
            var (statusCode, title) = MapException(exception);

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                // непредвиденные ошибки логируем с полным стектрейсом — это баги, их нужно расследовать
                _logger.LogError(exception, "Необработанное исключение");
            }
            else
            {
                // доменные исключения — это ожидаемые бизнес-сценарии, не баги, достаточно Warning
                _logger.LogWarning(exception, "Доменное исключение: {Message}", exception.Message);
            }

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Detail = exception.Message,
                Instance = httpContext.Request.Path
            };

            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: ct);

            return true; // сообщаем ASP.NET Core, что мы обработали исключение, дальше передавать не нужно
        }

        private static (int StatusCode, string Title) MapException(Exception exception) => exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Ресурс не найден"),
            ConflictException => (StatusCodes.Status409Conflict, "Конфликт состояния"),
            UnauthorizedDomainException => (StatusCodes.Status401Unauthorized, "Ошибка авторизации"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Некорректные данные"),
            _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера")
        };
    }
}
