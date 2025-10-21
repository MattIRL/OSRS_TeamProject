namespace OSRS_TeamProject.Services
{
    public record CatalogItem(int ItemId, string Name, string? Description);

    public interface IItemCatalog
    {
        Task<List<CatalogItem>> SearchAsync(string query, int take = 20);
        Task<CatalogItem?> GetByIdAsync(int itemId);
    }

    public static class OsrsIcon
    {
        public static string For(int itemId) =>
            $"https://static.runelite.net/cache/item/icon/{itemId}.png";
    }
}
