namespace Fintranet.Services.CongestionTax.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var commandName = request.GetGenericTypeName();
        _logger.LogInformation("----- Handling command {CommandName} ({@Command})", commandName
         , request);

        var response = await next();
        if (!FilterLogs.filterList.Any(p => p == commandName))
            _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", commandName, response);

        return response;
    }
}
public static class FilterLogs
{
    public static string[] filterList = { "GenerateTokenCommand", "AuthorizeCommand", "GenerateCurrentUserCalimsCommand", "GenerateAuthorizeUrlCommand" ,"LoginCommand"
    ,"AcceptUserCommand","GenerateAuthorizeUrlCommand"
    };

}
