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
               new InventoryItem { ItemId = 4151, Name = "Abyssal whip", Quantity = 1, Description = "A weapon from the Abyss.", IconUrl = ph, Notes = "Goal: 70 Attack" },
    new InventoryItem { ItemId = 1127, Name = "Rune platebody", Quantity = 1, Description = "Sturdy rune body armour.", IconUrl = ph, Notes = "Melee set" },
    new InventoryItem { ItemId = 379, Name = "Lobster", Quantity = 12, Description = "Cooked crustacean; food.", IconUrl = ph, Notes = "Food for slayer" },
    new InventoryItem { ItemId = 561, Name = "Nature rune", Quantity = 150, Description = "Rune used for alchemy.", IconUrl = ph, Notes = "High alch stack" },
    new InventoryItem { ItemId = 1521, Name = "Oak logs", Quantity = 80, Description = "Logs cut from an oak tree.", IconUrl = ph, Notes = "Fletching later" }
            );
        }
    }
}
