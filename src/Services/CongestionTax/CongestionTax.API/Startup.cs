
using FluentValidation;

namespace Fintranet.Services.CongestionTax.API;

public class Startup
{
  public Startup(IConfiguration configuration)
  {
    Configuration = configuration;
  }

  public IConfiguration Configuration { get; }

  // This method gets called by the runtime. Use this method to add services to the container.
  public void ConfigureServices(IServiceCollection services)
  {
    services
         .AddInfrastructure(Configuration)
         .AddApplication(Configuration)

         .AddCustomMVC(Configuration)
         .AddSwagger(Configuration)
         .AddCustomOptions(Configuration)
         .AddValidation(Configuration);
  }

  // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
  public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
  {
    var pathBase = Configuration["PATH_BASE"];

    if (!string.IsNullOrEmpty(pathBase))
    {
      loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
      app.UsePathBase(pathBase);
    }

    app.UseSwagger()
        .UseSwaggerUI(c =>
        {
          c.SwaggerEndpoint($"{(!string.IsNullOrEmpty(pathBase) ? pathBase : string.Empty)}/swagger/v1/swagger.json", "SSO.API V1");
        });

    if (env.IsDevelopment())
    {
      app.UseDeveloperExceptionPage();
    }
    else
    {
      app.UseExceptionHandler("/Home/Error");
    }
    app.UseRouting();

    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
      endpoints.MapControllers();
      endpoints.MapDefaultControllerRoute();
    });
  }
}

public static class CustomExtensionMethods
{
  public static IServiceCollection AddCustomOptions(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<AppSetting>(configuration);
    return services;
  }

  public static IServiceCollection AddValidation(this IServiceCollection services, IConfiguration configuration)
  {
    services.Configure<ApiBehaviorOptions>(options =>
    {
      options.ClientErrorMapping[404].Title = " Not Found Resource Or Api ";
      options.ClientErrorMapping[403].Title = "Forbidden";
      options.InvalidModelStateResponseFactory = context =>
          {

          var problemDetails = new ValidationProblemDetails(context.ModelState)
          {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = "Please refer to the errors property for additional details."
          };

          return new BadRequestObjectResult(problemDetails)
          {
            ContentTypes = { "application/problem+json", "application/problem+xml" }
          };
        };
    });

    services.AddValidatorsFromAssembly(typeof(ApplicationStartup).Assembly);

    services.AddFluentValidationAutoValidation(config =>
    {
      config.DisableDataAnnotationsValidation = true;
    });

    return services;
  }
  public static IServiceCollection AddCustomMVC(this IServiceCollection services, IConfiguration configuration)
  {

    services.AddControllers((options) =>
    {
      options.Filters.Add(typeof(HttpGlobalExceptionFilter));
    })
    .AddJsonOptions(options =>
    {
      options.JsonSerializerOptions.WriteIndented = true;
      options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    services.AddCors(options =>
    {
      options.AddPolicy("CorsPolicy",
              builder => builder
              .SetIsOriginAllowed((host) => true)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
    });

    return services;
  }
  public static IServiceCollection AddSwagger(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddSwaggerGen(c =>
    {
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Title = "Fintranet - Fintranet CongestionTax.API HTTP API",
        Version = "v1",
        Description = "The Fintranet Microservice HTTP API. This is a Data-Driven/CRUD microservice sample"
      });
      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
      {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
      });

      c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
        {
          new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
              {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
              },
              Scheme = "oauth2",
              Name = "Bearer",
              In = ParameterLocation.Header,

            },
            new List<string>()
          }
    });
      var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
      var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
      
    });

    return services;

  }
}
