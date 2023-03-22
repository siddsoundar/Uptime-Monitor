using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Uptime_Monitor.Models
{
    public class UptimeMonitorDatabaseSettings : IUptimeMonitorDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MonitoredUrlsCollectionName { get; set; } // Add this line
    }

    public interface IUptimeMonitorDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string MonitoredUrlsCollectionName { get; set; } // Add this line
    }
}
