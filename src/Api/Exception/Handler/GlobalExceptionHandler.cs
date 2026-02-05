using PeerlessInterview.src.Api.Response;

namespace PeerlessInterview.src.Api.Exception.Handler;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _nextInLine;
    private readonly ILogger<GlobalExceptionHandler> _logger;
    
    public GlobalExceptionHandler(RequestDelegate nextInLine, ILogger<GlobalExceptionHandler> logger)
    {
        _nextInLine = nextInLine;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _nextInLine(context);
        }
        catch(ValidationException ve)
        {
            _logger.LogError(ve, "Validation error occurred.");
            await HandleResponse(context, StatusCodes.Status400BadRequest, ve.Message);
        }
        catch(NotFoundException nfe)
        {
            _logger.LogError(nfe, "Not found error occurred.");
            await HandleResponse(context, StatusCodes.Status404NotFound, nfe.Message);
        }
        catch(AlreadyExistsException aee)
        {
            _logger.LogError(aee, "Already exists error occurred.");
            await HandleResponse(context, StatusCodes.Status409Conflict, aee.Message);
        }
        catch (System.Exception e)
        {
            _logger.LogError(e, "Unhandled exception occurred.");
            await HandleResponse(context, StatusCodes.Status500InternalServerError, "Server caught an unexpected error: " + e.Message);
        }
    }

    private async Task HandleResponse(HttpContext context, int status, string message)
    {
        context.Response.StatusCode = status;

        var rs = CommonResponse.Error(message);

        await context.Response.WriteAsJsonAsync(rs);
    }
}