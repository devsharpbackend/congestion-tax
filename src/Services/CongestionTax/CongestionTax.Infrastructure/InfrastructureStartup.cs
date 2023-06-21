
namespace Fintranet.Services.CongestionTax.Infrastructure;

public static class InfrastructureStartup
{
  public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration Configuration)
  {
    services.AddCustomDbContext(Configuration)
   .AddCustomOptions(Configuration);
    return services;
  }
  public static void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
  {
    app.UseHttpsRedirection();
  }
  public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddEntityFrameworkSqlServer()
       .AddDbContext<IApplicationDbContext,ApplicationDbContext>(options =>
       {
         options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                       sqlOptions.MigrationsAssembly(typeof(InfrastructureStartup).Assembly.GetName().Name);
                                       sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
       });
    return services;
  }
  public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
  {
    // Add  Error Handler 
    services.AddScoped<ICongestionTaxErrorHandler, CongestionTaxErrorHandler>();
    services.AddCommonErrorHandler(configuration);

    // Add Repository
    //services.AddScoped<IGatewayScopeRepository, GatewayScopeRepository>();
    //services.AddScoped<IClusterScopeRepository, ClusterScopeRepository>();
    //services.AddScoped<IRateLimitingPolicyRepository, RateLimitingPolicyRepository>();

    services.AddScoped<IApplicationDbContextSeed, ApplicationDbContextSeed>();
    return services;
  }
}