using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Uptime_Monitor.Models;

namespace Uptime_Monitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimePutController : ControllerBase
    {
        private readonly IMongoCollection<MonitoredUrl> _collection;

        public UptimePutController(IMongoClient client, UptimeMonitorDatabaseSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<MonitoredUrl>(settings.MonitoredUrlsCollectionName);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] MonitoredUrl monitoredUrl)
        {
            var result = await _collection.ReplaceOneAsync(x => x.Id == id, monitoredUrl);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
