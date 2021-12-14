using Newtonsoft.Json;

namespace short_url_service.Models
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "longurl")]
        public string LongUrl { get; set; }
        [JsonProperty(PropertyName = "shorturl")]
        public string ShortUrl { get; set; }
        [JsonProperty(PropertyName = "actiondate")]
        public DateTime ActionDate { get; set; }
        [JsonProperty(PropertyName = "domain")]
        public string   Domain { get; set; }

    }
}
