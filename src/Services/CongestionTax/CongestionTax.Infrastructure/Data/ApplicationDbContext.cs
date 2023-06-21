namespace Fintranet.Services.CongestionTax.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IUnitOfWork, IApplicationDbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
  {
  }
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
  {
    _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    System.Diagnostics.Debug.WriteLine("OrderingContext::ctor ->" + this.GetHashCode());
  }
  public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
  {
    await _mediator.DispatchCongestionTaxDomainEventsAsync(this);
    await this.SaveChangesAsync(cancellationToken);
    return true;
  }
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);
    //builder.ApplyConfiguration(new GatewayScopeEntityTypeConfiguration());
   
    // Ignore Domain Event Property from mapping
    var ListEntityMaps = builder.Model.GetEntityTypes();
    foreach (var EntityMap in ListEntityMaps)
    {
      EntityMap.AddIgnored("DomainEvents");
    }
  }
  private IDbContextTransaction? _currentTransaction;
  private readonly IMediator _mediator;
  public bool HasActiveTransaction => _currentTransaction != null;
  public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;
  public async Task<IDbContextTransaction> BeginTransactionAsync()
  {
    if (_currentTransaction != null) return null;

    _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

    return _currentTransaction;
  }
  public async Task CommitTransactionAsync(IDbContextTransaction transaction)
  {
    if (transaction == null) throw new ArgumentNullException(nameof(transaction));
    if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

    try
    {
      await SaveChangesAsync();
      transaction.Commit();
    }
    catch
    {
      RollbackTransaction();
      throw;
    }
    finally
    {
      if (_currentTransaction != null)
      {
        _currentTransaction.Dispose();
        _currentTransaction = null;
      }
    }
  }
  public void RollbackTransaction()
  {
    try
    {
      _currentTransaction?.Rollback();
    }
    finally
    {
      if (_currentTransaction != null)
      {
        _currentTransaction.Dispose();
        _currentTransaction = null;
      }
    }
  }

  public IExecutionStrategy CreateStrategy() => Database.CreateExecutionStrategy();

}
class NoMediator : IMediator
{
  public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return null;
  }

  public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
  {
    return null;
  }

  public Task Publish(object notification, CancellationToken cancellationToken = default)
  {
    return Task.CompletedTask;
  }

  public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
  {
    return Task.CompletedTask;
  }

  public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
  {
    return (Task<TResponse>)Task.CompletedTask;
  }

  public Task<object?> Send(object request, CancellationToken cancellationToken = default)
  {
    return (Task<object?>)Task.CompletedTask;
  }

  public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
  {
    return (Task<object?>)Task.CompletedTask;
  }
}
