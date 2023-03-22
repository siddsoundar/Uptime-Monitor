using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Uptime_Monitor.Models.MonitoredUrlsRepository;
using Uptime_Monitor.Controllers;

namespace Uptime_Monitor.Models
{
    public class MonitoredUrlsRepository : IMonitoredUrlsRepository
    {
        private readonly MonitoredUrlsDatabaseContext _context;
        private readonly UptimeDeleteController _deleteController;
        private readonly UptimePutController _putController;

        public MonitoredUrlsRepository(MonitoredUrlsDatabaseContext context, UptimeDeleteController deleteController, UptimePutController putController)
        {
            _context = context;
            _deleteController = deleteController;
            _putController = putController;
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

        public async Task<bool> UpdateUrl(string id, MonitoredUrl url)
        {
            var response = await _putController.Update(id, url);
            return response is Microsoft.AspNetCore.Mvc.NoContentResult;
        }

        public async Task<bool> DeleteUrl(string id)
        {
            var response = await _deleteController.Delete(id);
            return response is Microsoft.AspNetCore.Mvc.NoContentResult;
        }

        public async Task DeleteMonitoredUrl(string id)
        {
            var filter = Builders<MonitoredUrl>.Filter.Eq(x => x.Id, id);
            await _context.MonitoredUrls.DeleteOneAsync(filter);
        }

        public interface IMonitoredUrlsRepository
        {
            Task<MonitoredUrl> GetMonitoredUrlById(string id);
            Task<List<MonitoredUrl>> GetAllMonitoredUrls();
            Task<MonitoredUrl> CreateMonitoredUrl(MonitoredUrl url);
            Task<bool> UpdateUrl(string id, MonitoredUrl url);
            Task<bool> DeleteUrl(string id);
            Task DeleteMonitoredUrl(string id);
        }
    }
}
