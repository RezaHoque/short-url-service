namespace short_url_service.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string LongUrl { get; set; }
        public string ShortUrl { get; set; }
        public DateTime ActionDate { get; set; }
        public string   Domain { get; set; }

    }
}
