namespace Fintranet.BuildingBlocks.Common.Infrastructure.ErrorHandler;
public static class ErrorHandlerStartup
{
    public static IServiceCollection AddCommonErrorHandler(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IErrorHandler, ErrorHandler>();
       
        return services;
    }
}
