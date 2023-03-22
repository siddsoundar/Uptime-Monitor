using Uptime_Monitor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using static Uptime_Monitor.Models.MonitoredUrlsRepository;

namespace Uptime_Monitor
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure the UptimeMonitorDatabaseSettings options class to use the "UptimeMonitorDatabaseSettings" section of the appsettings.json file
            services.Configure<UptimeMonitorDatabaseSettings>(_configuration.GetSection("UptimeMonitorDatabaseSettings"));

            // Register MongoClient as a singleton
            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<UptimeMonitorDatabaseSettings>>();
                return new MongoClient(settings.Value.ConnectionString);
            });

            // Register MonitoredUrlsDatabaseContext and MonitoredUrlsRepository as scoped services
            services.AddScoped<IMonitoredUrlsDatabaseContext, MonitoredUrlsDatabaseContext>();
            services.AddScoped<IMonitoredUrlsRepository, MonitoredUrlsRepository>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Omitted for brevity
        }
    }
}
