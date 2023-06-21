namespace Fintranet.BuildingBlocks.Common.Application;

public static class ApplicationStartup
{
  public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration Configuration)
  {
    services.AddCQRS(Configuration);
    services.AddCustomOptions(Configuration);
    return services;

  }
}

public static class CustomExtensionMethods
{
  public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
  {

    services.Configure<AppSetting>(configuration);
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
  
    return services;
  }
  public static IServiceCollection AddCQRS(this IServiceCollection services, IConfiguration configuration)
  {
    
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionBehavior<,>));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));
    services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationStartup).Assembly));
  
    return services;
  }


}



