namespace short_url_service.Services
{
    public class UtilityService:IUtilityService
    {
        public string GenerateShorturl(string longUrl)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            GuidString = GuidString.Replace("/", "");

            //var d = "reza.me/";
            Uri uri = new Uri(longUrl);
            string requested = uri.Scheme + Uri.SchemeDelimiter;

            var shortUrl = requested + uri.Host +"/s/"+ GuidString;

            return shortUrl;
        }
    }
}
