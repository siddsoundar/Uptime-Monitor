using MongoDB.Driver;
using Uptime_Monitor.Models;

namespace Uptime_Monitor.Models
{
    public class MonitoredUrlsDatabaseContext : IMonitoredUrlsDatabaseContext
    {
        private readonly IMongoDatabase _database;

        public MonitoredUrlsDatabaseContext(UptimeMonitorDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            _database = client.GetDatabase(settings.DatabaseName);
        }

        public IMongoCollection<MonitoredUrl> MonitoredUrls => _database.GetCollection<MonitoredUrl>("monitoredUrls");
    }


    public interface IMonitoredUrlsDatabaseContext
    {
        IMongoCollection<MonitoredUrl> MonitoredUrls { get; }
    }
    
}
