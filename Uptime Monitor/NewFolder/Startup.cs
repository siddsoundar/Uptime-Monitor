using Uptime_Monitor.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Microsoft.Extensions.Options;

namespace Uptime_Monitor
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<UptimeMonitorDatabaseSettings>(
                _config.GetSection(nameof(UptimeMonitorDatabaseSettings)));

            services.AddSingleton<IMongoClient>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<UptimeMonitorDatabaseSettings>>();
                return new MongoClient(settings.Value.ConnectionString);
            });

            services.AddSingleton<IMonitoredUrlsDatabaseContext, MonitoredUrlsDatabaseContext>();
            services.AddSingleton<IMonitoredUrlsRepository, MonitoredUrlsRepository>();
        }
    }
}
