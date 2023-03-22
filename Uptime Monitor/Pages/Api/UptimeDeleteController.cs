using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Uptime_Monitor.Models;

namespace Uptime_Monitor.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimeDeleteController : ControllerBase
    {
        private readonly IMongoCollection<MonitoredUrl> _collection;

        public UptimeDeleteController(IMongoClient client, UptimeMonitorDatabaseSettings settings)
        {
            var database = client.GetDatabase(settings.DatabaseName);
            _collection = database.GetCollection<MonitoredUrl>(settings.MonitoredUrlsCollectionName);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _collection.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
