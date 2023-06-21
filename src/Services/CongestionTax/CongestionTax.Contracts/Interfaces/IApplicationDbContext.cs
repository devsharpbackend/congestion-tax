
namespace Fintranet.Services.CongestionTax.Contracts.Interfaces;

public interface IApplicationDbContext
{
  bool HasActiveTransaction { get; }

  Task<IDbContextTransaction> BeginTransactionAsync();
  Task CommitTransactionAsync(IDbContextTransaction transaction);
  IExecutionStrategy CreateStrategy();
  IDbContextTransaction? GetCurrentTransaction();
  void RollbackTransaction();
  Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default);
}
