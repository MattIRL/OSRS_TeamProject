using Microsoft.EntityFrameworkCore;
using OSRS_TeamProject.Models;

namespace OSRS_TeamProject.Data
{
    public class GameInvContext : DbContext
    {
        public GameInvContext(DbContextOptions<GameInvContext> options) : base(options) { }

        public DbSet<InventoryItem> Inventory => Set<InventoryItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            const string ph = "/img/placeholder-item.svg";

            modelBuilder.Entity<InventoryItem>().HasKey(x => x.ItemId);
            modelBuilder.Entity<InventoryItem>().HasData(
                new InventoryItem { ItemId = 4151, Name = "Abyssal whip", Quantity = 1, Notes = "Goal: 70 Attack", IconUrl = ph },
                new InventoryItem { ItemId = 1127, Name = "Rune platebody", Quantity = 1, Notes = "Melee set", IconUrl = ph },
                new InventoryItem { ItemId = 379, Name = "Lobster", Quantity = 12, Notes = "Food for slayer", IconUrl = ph },
                new InventoryItem { ItemId = 561, Name = "Nature rune", Quantity = 150, Notes = "High alch stack", IconUrl = ph },
                new InventoryItem { ItemId = 1521, Name = "Oak logs", Quantity = 80, Notes = "Fletching later", IconUrl = ph }
            );
        }
    }
}
