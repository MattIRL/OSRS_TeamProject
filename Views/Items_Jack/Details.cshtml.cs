using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OSRS_TeamProject.Data;
using OSRS_TeamProject.Models;

namespace OSRS_TeamProject.Views.Items_Jack
{
    public class DetailsModel : PageModel
    {
        private readonly OSRS_TeamProject.Data.GameInvContext _context;

        public DetailsModel(OSRS_TeamProject.Data.GameInvContext context)
        {
            _context = context;
        }

        public InventoryItem InventoryItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryitem = await _context.Inventory.FirstOrDefaultAsync(m => m.ItemId == id);
            if (inventoryitem == null)
            {
                return NotFound();
            }
            else
            {
                InventoryItem = inventoryitem;
            }
            return Page();
        }
    }
}
