using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Uptime_Monitor.Models;

namespace Uptime_Monitor.Models
{
    public class MonitoredUrlsRepository
    {
        private readonly MonitoredUrlsDatabaseContext _context;

        public MonitoredUrlsRepository(MonitoredUrlsDatabaseContext context)
        {
            _context = context;
        }

        public async Task<MonitoredUrl> GetMonitoredUrlById(string id)
        {
            var filter = Builders<MonitoredUrl>.Filter.Eq(x => x.Id, id);
            var result = await _context.MonitoredUrls.FindAsync(filter);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<List<MonitoredUrl>> GetAllMonitoredUrls()
        {
            var filter = Builders<MonitoredUrl>.Filter.Empty;
            var result = await _context.MonitoredUrls.FindAsync(filter);
            return await result.ToListAsync();
        }

        public async Task<MonitoredUrl> CreateMonitoredUrl(MonitoredUrl url)
        {
            url.Id = Guid.NewGuid().ToString();
            await _context.MonitoredUrls.InsertOneAsync(url);
            return url;
        }

        public async Task UpdateMonitoredUrl(string id, MonitoredUrl url)
        {
            url.Id = id;
            await _context.MonitoredUrls.ReplaceOneAsync(x => x.Id == id, url);
        }

        public async Task DeleteMonitoredUrl(string id)
        {
            var filter = Builders<MonitoredUrl>.Filter.Eq(x => x.Id, id);
            await _context.MonitoredUrls.DeleteOneAsync(filter);
        }
    }
}
