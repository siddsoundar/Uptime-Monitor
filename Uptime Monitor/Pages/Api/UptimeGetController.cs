using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Uptime_Monitor.Models;

namespace Uptime_Monitor.Pages.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimeGetController : ControllerBase
    {
        private readonly IMongoDatabase _database;

        public UptimeGetController(IMongoDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUrls()
        {
            var collection = _database.GetCollection<MonitoredUrl>("urls");
            var urls = await collection.Find(_ => true).ToListAsync();
            return Ok(urls);
        }
    }
}
