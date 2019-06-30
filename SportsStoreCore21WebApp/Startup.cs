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
      services.AddMvc();

      services.AddDbContext<SportsStoreDbContext>(cfg => {
        //Getting data form Appsetting this will change KeyVault

        cfg.UseSqlServer(Configuration.GetConnectionString("SportStoreDbConnection"), sqlServerOptionsAction: sqlOption => {
          sqlOption.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
        });
      });
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
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
