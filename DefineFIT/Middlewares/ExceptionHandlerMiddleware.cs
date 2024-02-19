using DefineFIT.Domain.Common.Exceptions;
using DefineFIT.Domain.Common.Extensions;
using System.Net;
using System.Text.Json;
using InvalidDataException = DefineFIT.Domain.Common.Exceptions.InvalidDataException;


namespace DefineFIT.Api.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
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
        catch (Exception e)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            switch (e)
            {
                case MissingRequiredFilterException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                case EntityAlreadyExistsException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case EntityNotFoundException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case TokenValidationException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case InvalidDataException:
                    _logger.LogWarning(e, $"Expected error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case IBusinessException:
                    _logger.LogWarning(e, $"Business error: {e.Message}");
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
                default:
                    _logger.LogError(e, $"Unexpected error: {e.Message}");
                    SentrySdk.CaptureException(e);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            await response.WriteAsync(JsonSerializer.Serialize(
                new BaseException
                {
                    ContextTraceId = Guid.NewGuid().ToString(),
                    Errors = e.BuildErrors()
                }));
        }
    }
}