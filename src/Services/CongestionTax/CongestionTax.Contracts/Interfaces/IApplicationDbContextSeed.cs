
namespace Fintranet.Services.CongestionTax.Contracts.Interfaces;
public interface IApplicationDbContextSeed
{
  Task MigrateAndSeedAsync(IApplicationDbContext context, ILogger<IApplicationDbContextSeed> logger);
}
