// Services/RemoteItemCatalog.cs
namespace OSRS_TeamProject.Services
{
    public record OsrsMappingItem(int id, string name, string? examine);

    public class RemoteItemCatalog : IItemCatalog
    {
        private readonly HttpClient _http;
        private readonly Lazy<Task<List<CatalogItem>>> _cache;

        public RemoteItemCatalog(HttpClient http)
        {
            _http = http;
            _cache = new(() => LoadAllAsync());
        }

        private async Task<List<CatalogItem>> LoadAllAsync()
        {
            try
            {
                using var req = new HttpRequestMessage(HttpMethod.Get, "https://prices.runescape.wiki/api/v1/osrs/mapping");
                // extra safety if this HttpClient wasn't configured (should be via DI above)
                if (!_http.DefaultRequestHeaders.UserAgent.Any())
                    req.Headers.UserAgent.ParseAdd("OSRS_TeamProject/1.0 (+your-email@example.com)");
                req.Headers.Accept.ParseAdd("application/json");

                using var resp = await _http.SendAsync(req, HttpCompletionOption.ResponseHeadersRead);
                if (!resp.IsSuccessStatusCode)
                {
                    // 403, 5xx, etc. — don’t throw, just return empty so UI keeps working
                    return new List<CatalogItem>();
                }

                var list = await resp.Content.ReadFromJsonAsync<List<OsrsMappingItem>>() ?? new();
                return list.Select(x => new CatalogItem(x.id, x.name, x.examine)).ToList();
            }
            catch
            {
                return new List<CatalogItem>();
            }
        }

        public async Task<List<CatalogItem>> SearchAsync(string query, int take = 20)
        {
            if (string.IsNullOrWhiteSpace(query)) return new();
            var data = await _cache.Value;
            var q = query.Trim();
            return data.Where(i => i.Name.Contains(q, StringComparison.OrdinalIgnoreCase))
                       .Take(take).ToList();
        }

        public async Task<CatalogItem?> GetByIdAsync(int itemId)
        {
            var data = await _cache.Value;
            return data.FirstOrDefault(i => i.ItemId == itemId);
        }
    }
}
