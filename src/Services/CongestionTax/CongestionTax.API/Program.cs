

var configuration = GetConfiguration();

try
{
  var host = CreateHostBuilder(configuration, args);
  Log.Information("Configuring web host ({ApplicationContext})...", Program.AppName);
  Log.Information("Applying migrations ({ApplicationContext})...", Program.AppName);

   // Apply Migrations
  host.MigrateDataBase<IApplicationDbContext>((services) =>
  {
      var logger = services.GetService<ILogger<IApplicationDbContextSeed>>();
      var context = services.GetRequiredService<IApplicationDbContext>();
      var seed = services.GetRequiredService<IApplicationDbContextSeed>();
      seed.MigrateAndSeedAsync(context, logger).Wait();
  });

  Log.Information("Starting web host ({ApplicationContext})...", Program.AppName);
  host.Run();

}
catch (Exception ex)
{
  Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
  Log.CloseAndFlush();
}

IConfiguration GetConfiguration()
{
  var path = Directory.GetCurrentDirectory();

  var builder = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddEnvironmentVariables();

  return builder.Build();
}

IHost CreateHostBuilder(IConfiguration configuration, string[] args) =>

Host.CreateDefaultBuilder(args)
   .ConfigureWebHostDefaults(webBuilder =>
   {
     webBuilder.UseStartup<Startup>()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration(x => x.AddConfiguration(configuration))
    .CaptureStartupErrors(false);
   })
   .UseSerilog(SeriLogger.Configure)
   .Build();

public partial class Program
{
  public static string? Namespace = typeof(Startup).Namespace;
  public static string? AppName = "CongestionTax.API";
}
