using System.ComponentModel.DataAnnotations;

namespace OSRS_TeamProject.Models
{
    public class InventoryItem
    {
        [Key] public int ItemId { get; set; }

        [Required, StringLength(80, MinimumLength = 2)]
        public string Name { get; set; } = "";

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(300)]
        public string? Description { get; set; }   // <-- new

        [StringLength(500)]
        public string? Notes { get; set; }

        [StringLength(300)]
        public string? IconUrl { get; set; }
    }
}
