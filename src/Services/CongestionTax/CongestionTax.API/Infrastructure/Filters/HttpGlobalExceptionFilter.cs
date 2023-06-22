namespace Fintranet.Services.CongestionTaxA.API.Infrastructure.Filters;
public class HttpGlobalExceptionFilter : IExceptionFilter
{

    private readonly ILogger<HttpGlobalExceptionFilter> logger;
    private readonly IErrorHandler _errorHandler;
    public HttpGlobalExceptionFilter(IErrorHandler errorHandler, ILogger<HttpGlobalExceptionFilter> logger)
    {
        _errorHandler = errorHandler;
        this.logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            message: context.Exception.Message);

        context.ExceptionHandled = true;

        if (context.Exception.GetType() == typeof(CongestionTaxApplicationException))
        {
            CongestionTaxApplicationException? congestionTaxApplicationException = context.Exception as CongestionTaxApplicationException;
            context.HttpContext.Response.StatusCode = congestionTaxApplicationException.JsonErrorResponse.StatusCode;
            context.Result = new ObjectResult(congestionTaxApplicationException.JsonErrorResponse);

            return;
        }

        var res = _errorHandler.GetError(context.Exception);
        context.HttpContext.Response.StatusCode = res.StatusCode;
        context.Result = new ObjectResult(res);

    }
}
