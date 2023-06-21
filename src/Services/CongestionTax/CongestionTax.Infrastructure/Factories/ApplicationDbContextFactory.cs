namespace Fintranet.Services.CongestionTax.Infrastructure.Factories;
public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var migrationsAssembly = typeof(InfrastructureStartup).GetTypeInfo().Assembly.GetName().Name;

        var config = new ConfigurationBuilder()
           .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
           .AddJsonFile("appsettings.json")
           .AddEnvironmentVariables()
           .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        optionsBuilder.UseSqlServer(config["ConnectionString"], sqlServerOptionsAction: o => o.MigrationsAssembly(migrationsAssembly));

        return new ApplicationDbContext(optionsBuilder.Options,new NoMediator());
    }
}