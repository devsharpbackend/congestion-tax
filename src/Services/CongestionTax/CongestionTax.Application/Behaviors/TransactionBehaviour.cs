namespace Fintranet.Services.CongestionTax.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : TransactionRequest<TResponse>
{
  private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
  private readonly IApplicationDbContext _dbContext;
  public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IApplicationDbContext dbContext)
  {
    _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
  }

  public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
  {
    var response = default(TResponse);
    var typeName = request.GetGenericTypeName();
    if (FilterTransactions.filterList.Any(p => p == typeName))
    {
      return await next();
    }
    try
    {
      if (_dbContext.HasActiveTransaction)
      {
        return await next();
      }

      var strategy = _dbContext.CreateStrategy();
      await strategy.ExecuteAsync(async () =>
      {
        Guid transactionId;
        using var transaction = await _dbContext.BeginTransactionAsync();
        using (LogContext.PushProperty("TransactionContext", transaction.TransactionId))
        {
          _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);

          response = await next();

          _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

          await _dbContext.CommitTransactionAsync(transaction);

          transactionId = transaction.TransactionId;
        }
      });

      return response;
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
      throw;
    }
  }
}
public static class FilterTransactions
{
  public static string[] filterList = {
    };

}
