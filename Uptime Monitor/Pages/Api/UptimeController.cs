using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Uptime_Monitor.Pages.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UptimeController : ControllerBase
    {
        private readonly IMongoDatabase _database;

        public UptimeController(IMongoDatabase database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<IActionResult> AddUrl([FromBody] string url)
        {
            var collection = _database.GetCollection<BsonDocument>("urls");
            var document = new BsonDocument
            {
                { "url", url },
                { "status", "unknown" }
            };
            await collection.InsertOneAsync(document);
            return Ok();
        }
    }
}
