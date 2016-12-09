using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using StarterAPI.Controllers;
using StarterAPI.Repository;
using StarterAPI.Services;
using NLog.Extensions.Logging;

namespace StarterAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
        
            services.AddEntityFrameworkSqlServer()
                .AddDbContext<DataContext>(options =>
                    options.UseSqlServer(Configuration["Data:SampleDBContext:ConnectionString"]));

            // new instance created once for each web request
            services.AddScoped(typeof(IRepository<>), typeof(DataRepository<>));

            // new instance created each time requested
            services.AddTransient<ISampleService, SampleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // Console logging configuration set in appsettings
            loggerFactory.AddConsole(Configuration.GetSection("Data:Logging:LogLevel"));
            // Log all trace information to debug window
            loggerFactory.AddDebug(LogLevel.Trace);

            // Connection String Must Be Configured to Use NLog - Once set uncomment the 2 lines below

            // loggerFactory.AddNLog();
            // env.ConfigureNLog("nlog.config");

            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseDeveloperExceptionPage();
            app.UseBrowserLink();

            app.UseMvc();
        }
    }
}
