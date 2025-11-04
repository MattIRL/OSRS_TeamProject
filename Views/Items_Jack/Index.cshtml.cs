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
    public class IndexModel : PageModel
    {
        private readonly OSRS_TeamProject.Data.GameInvContext _context;

        public IndexModel(OSRS_TeamProject.Data.GameInvContext context)
        {
            _context = context;
        }

        public IList<InventoryItem> InventoryItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            InventoryItem = await _context.Inventory.ToListAsync();
        }
    }
}
