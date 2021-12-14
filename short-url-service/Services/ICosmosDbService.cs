using short_url_service.Models;

namespace short_url_service.Services
{
    public interface ICosmosDbService
    {
        Task AddItemAsync(Item item);
        Task<IEnumerable<Item>> GetItemsAsync(string query);
    }
}
