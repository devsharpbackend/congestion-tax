
namespace Fintranet.Services.CongestionTax.Infrastructure.Factories;

public class ApplicationDbContextSeed : IApplicationDbContextSeed
{

  public async Task MigrateAndSeedAsync(IApplicationDbContext _context, ILogger<IApplicationDbContextSeed> logger)
  {
    ApplicationDbContext context = _context as ApplicationDbContext;
    if (context == null)
      return;

    await context.Database.MigrateAsync();
    try
    {
      // Seed Data
      //if (!context.Set<SessionAffinityPolicy>().Any())
      //{
      //  context.AddRange(GetPredefinedSessionAffinityPolicy());
      //}
      //if (!context.Set<SessionAffinityFailurePolicy>().Any())
      //{
      //  context.AddRange(GetPredefinedSessionAffinityFailurePolicy());
      //}
      //if (!context.Set<LoadBalancingPolicy>().Any())
      //{
      //  context.AddRange(GetPredefinedLoadBalancingPolicy());
      //}
      //if (!context.Set<RateLimitingType>().Any())
      //{
      //  context.AddRange(GetPredefinedRateLimitingType());
      //}
      await context.SaveChangesAsync();
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(ApplicationDbContext));
    }
  }

  //private IEnumerable<SessionAffinityPolicy> GetPredefinedSessionAffinityPolicy()
  //{
  //  return new List<SessionAffinityPolicy>()
  //      {
  //          SessionAffinityPolicy.ArrCookie,
  //          SessionAffinityPolicy.Cookie,
  //          SessionAffinityPolicy.HashCookie,
  //          SessionAffinityPolicy.CustomHeader
  //      };
  //}
}
