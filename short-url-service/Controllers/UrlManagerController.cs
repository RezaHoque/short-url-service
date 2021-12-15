using Microsoft.AspNetCore.Mvc;
using short_url_service.Models;
using short_url_service.Services;

namespace short_url_service.Controllers
{
   
    [ApiVersion("1.0")]
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    public class UrlManagerController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ICosmosDbService _cosmosDbService;
        private readonly IUtilityService _utilityService;
        public UrlManagerController(IConfiguration configuration, IUtilityService utilityService)
        {
            _config = configuration;
            _cosmosDbService =InitializeCosmos.InitializeCosmosClientInstanceAsync(_config.GetSection("CosmosDb")).GetAwaiter().GetResult();
            _utilityService = utilityService;
        }
        [Route("/getshort")]
        [HttpPost]
        public async Task<IActionResult> GetShort(RequestModel model)
        {
            if (!string.IsNullOrEmpty(model.Url))
            {
                var existingItems = _cosmosDbService.GetItemsAsync($"SELECT * FROM c WHERE c.longurl='{model.Url}'").GetAwaiter().GetResult();

                if (existingItems != null && existingItems.Any())
                {
                    return Ok(existingItems.First());
                }
                else
                {
                    var data = new Item();
                    data.ShortUrl = _utilityService.GenerateShorturl(model.Url);
                    data.LongUrl = model.Url;
                    data.ActionDate = DateTime.Now;
                    data.Id = Guid.NewGuid().ToString();

                    Uri uri = new Uri(model.Url);
                    data.Domain = uri.Host;

                    await _cosmosDbService.AddItemAsync(data);

                    return Ok(data);
                }
            }
            return BadRequest();
        }
        [Route("/getlong")]
        [HttpGet]
        public IActionResult GetLong(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                var items= _cosmosDbService.GetItemsAsync($"SELECT * FROM c WHERE c.shorturl='{url}'").GetAwaiter().GetResult();

                if (items != null)
                {
                    return Ok(items);
                }
                else
                {
                    return Ok("Nothing found");
                }
            }
            return BadRequest();
        }
    }
}
