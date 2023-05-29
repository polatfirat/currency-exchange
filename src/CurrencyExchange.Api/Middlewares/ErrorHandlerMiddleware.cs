using CurrencyExchange.Domain.Exceptions;
using System.Net;
using System;
using System.Text.Json;

namespace Customer.Api.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
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
            catch (Exception exception)
            {
                var errorResult = new ErrorResult
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                };

                switch (exception)
                {
                    case DatabaseException e:
                        errorResult.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.ErrorMessage = e.Message;
                        _logger.LogError(e.Message);
                        break;

                    case OperationException e:
                        errorResult.ErrorCode = (int)HttpStatusCode.Forbidden;
                        errorResult.ErrorMessage = e.Message;
                        _logger.LogError(e.Message);
                        break;
                    case ValidationException e:
                        errorResult.ErrorCode = (int)HttpStatusCode.BadRequest;
                        errorResult.ErrorMessage = e.Message;
                        _logger.LogError(e.Message);
                        break;
                    default:
                        errorResult.ErrorCode = (int)HttpStatusCode.InternalServerError;
                        errorResult.ErrorMessage = exception.Message;
                        _logger.LogError(exception.Message);
                        break;
                }

                var response = context.Response;
                response.ContentType = "application/json";
                response.StatusCode = errorResult.ErrorCode;
                await response.WriteAsync(JsonSerializer.Serialize(errorResult));
            }
        }
    }
}
