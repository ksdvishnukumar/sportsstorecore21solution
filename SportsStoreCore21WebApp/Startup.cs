using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;


using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Routing;
using SportsStoreCore21WebApp.Models;
using Microsoft.EntityFrameworkCore;
using SportsStoreCore21WebApp.Models.Abstract;
using SportsStoreCore21WebApp.Models.Concrete;
using SportsStoreCore21WebApp.Models.Services;
using Microsoft.Extensions.Logging.ApplicationInsights;

namespace SportsStoreCore21WebApp
{
  public class Startup
  {
    private readonly ILogger<Startup> _logger;

    public Startup(IConfiguration configuration, ILogger<Startup> logger)
    {
      Configuration = configuration;
      _logger = logger;
    }
    public IConfiguration Configuration { get; private set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.Configure<StorageUtility>(cfg => {
        if (string.IsNullOrEmpty(Configuration["StorageAccountInformation"]))
        {
          cfg.StorageAccountName = Configuration["StorageAccountInformation:StorageAccountName"];
          cfg.StorageAccountAccessKey = Configuration["StorageAccountInformation:StorageAccountAccessKey"];
        }
      });

      services.AddMvc();

      //cfg.UseSqlServer(Configuration.GetConnectionString("SportsStoreDbConnection"),
      services.AddDbContext<SportsStoreDbContext>(cfg => {
        //Getting data form Appsetting this will change KeyVault ConnectionStrings--SportsStoreDbConnection
        cfg.UseSqlServer(Configuration["ConnectionStrings:SportsStoreDbConnection"], sqlServerOptionsAction: sqlOption => {
          sqlOption.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
      });

      if (Configuration["EnableRedisCaching"] == "true")
      {
        services.AddDistributedRedisCache(cfg => {
          cfg.Configuration = Configuration["ConnectionStrings:RedisConnection"];
          cfg.InstanceName = "master";
        });
      }

      services.AddScoped<IProductRepository, EfProductRepository>();
      services.AddScoped<IPhotoService, PhotoService>();

      services.AddApplicationInsightsTelemetry(cfg => {
        cfg.InstrumentationKey = Configuration["ApplicationInsights:InstrumentationKey"];
      });
      services.AddLogging(cfg =>
      {
        cfg.AddApplicationInsights(Configuration["ApplicationInsights:InstrumentationKey"]);
        // Optional: Apply filters to configure LogLevel Information or above is sent to
        // ApplicationInsights for all categories.
        cfg.AddFilter<ApplicationInsightsLoggerProvider>("", LogLevel.Information);

        // Additional filtering For category starting in "Microsoft",
        // only Warning or above will be sent to Application Insights.
        //cfg.AddFilter<ApplicationInsightsLoggerProvider>("Microsoft", LogLevel.Warning);
      });

    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      var appInsightsFlag = app.ApplicationServices.GetService<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>();
      if (Configuration["EnableAppInsightsDisableTelemetry"] == "false")
      {
        appInsightsFlag.DisableTelemetry = false;
      }
      else
      {
        appInsightsFlag.DisableTelemetry = true;
      }

      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseFileServer();

      app.UseMvc(ConfigureRoutes);

      using (var scope = app.ApplicationServices.CreateScope())
      {
        var context = scope.ServiceProvider.GetRequiredService<SportsStoreDbContext>();

        //This will ensure the database is created and the migrations are applied
        context.Database.Migrate();

        // If Database does not exist then the database and all its schema are created 
        //context.Database.EnsureCreated();
      }


      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello World!");
      });
    }

    private void ConfigureRoutes(IRouteBuilder routeBuilder)
    {
      routeBuilder.MapRoute("Default", "{controller=Product}/{action=Index}/{id?}");
      //routeBuilder.MapRoute("Default", "{controller}/{action}/{id?}");
    }
  }
}
