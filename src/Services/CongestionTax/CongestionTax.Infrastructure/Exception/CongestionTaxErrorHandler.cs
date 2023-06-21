namespace Fintranet.Services.CongestionTax.Infrastructure.Exceptions;

public class CongestionTaxErrorHandler : ICongestionTaxErrorHandler
{
  private readonly ILogger<CongestionTaxErrorHandler> logger;
  private readonly IErrorHandler _errorHandler;
  private readonly IWebHostEnvironment _env;
  public CongestionTaxErrorHandler(IErrorHandler errorHandler, IWebHostEnvironment env, ILogger<CongestionTaxErrorHandler> logger)
  {
    _errorHandler = errorHandler;
    this.logger = logger;
    _env = env;
  }
  public JsonErrorResponse GetError(Exception Exception)
  {
    JsonErrorResponse jsonErrorResponse = new JsonErrorResponse();
    if (_env.IsDevelopment())
    {
      jsonErrorResponse.DeveloperMessage = Exception.ToString();
    }

    if (Exception.GetType() == typeof(NotFoundException))
    {
      jsonErrorResponse.Messages = new string[] { Exception.Message };
      jsonErrorResponse.StatusCode = StatusCodes.Status404NotFound;
      return jsonErrorResponse;
    }

    // Manage ValidationException
    if (Exception?.GetType() == typeof(ValidationException))
    {
      var validationException = Exception as ValidationException;
      var problemDetails = new ValidationProblemDetails()
      {
        //Instance = context.HttpContext.Request.Path,
        Status = StatusCodes.Status400BadRequest,
        Detail = "Please refer to the errors property for additional details."
      };

      foreach (var error in validationException.Errors)
      {
        problemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
      }

      jsonErrorResponse.Messages = problemDetails;
      jsonErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;
      return jsonErrorResponse;
    }

    // Manage Domain Exception
    if (Exception.GetType() == typeof(CongestionTaxDomainException))
    {
      if (Exception.InnerException != null)
      {
        if (Exception.InnerException.GetType() == typeof(ConflictException))
        {
          var conflictException = Exception.InnerException as ConflictException;
          jsonErrorResponse.Messages = conflictException.Message;
          jsonErrorResponse.StatusCode = (int)HttpStatusCode.Conflict;
          return jsonErrorResponse;
        }
      }

      var problemDetails = new ValidationProblemDetails()
      {
        Status = StatusCodes.Status400BadRequest,
        Detail = "Please refer to the errors property for additional details."
      };
      problemDetails.Errors.Add("DomainValidations", new string[] { Exception.Message.ToString() });
      jsonErrorResponse.Messages = problemDetails;
      jsonErrorResponse.StatusCode = (int)HttpStatusCode.BadRequest;

      return jsonErrorResponse;
    }


    return _errorHandler.GetError(Exception); ;
  }
}
