using System;

namespace Uptime_Monitor.Models
{
    public class MonitoredUrl
    {
        public string Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public bool IsMonitored { get; set; }
        public DateTime LastChecked { get; set; }
        public bool IsUp { get; set; }
        public double ResponseTime { get; set; }
    }
}
