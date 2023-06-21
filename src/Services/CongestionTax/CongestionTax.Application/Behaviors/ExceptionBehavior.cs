
namespace Fintranet.Services.CongestionTax.Application.Behaviors;
public class ExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
{
    private readonly ILogger<ExceptionBehavior<TRequest, TResponse>> _logger;
    private readonly ICongestionTaxErrorHandler _congestionTaxErrorHandler;
    public ExceptionBehavior(ILogger<ExceptionBehavior<TRequest, TResponse>> logger,
        ICongestionTaxErrorHandler congestionTaxErrorHandler)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _congestionTaxErrorHandler = congestionTaxErrorHandler ?? throw new ArgumentNullException(nameof(congestionTaxErrorHandler));
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName()
          , request);

            var response = await next();

            _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }

        catch (Exception ex)
        {
            _logger.LogError("----- Error in  Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), ex.ToString());
            throw new CongestionTaxApplicationException(ex, _congestionTaxErrorHandler);
        }
    }
}
