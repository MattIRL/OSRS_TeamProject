using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSRS_TeamProject.Data;
using OSRS_TeamProject.Models;
using OSRS_TeamProject.Services; // for OsrsIcon

namespace OSRS_TeamProject.Controllers
{
    [Route("liam")]
    public class Items_LiamController : Controller
    {
        private readonly GameInvContext _context;
        public Items_LiamController(GameInvContext context) => _context = context;

        // GET /liam
        [HttpGet("")]
        public async Task<IActionResult> Index(string? search, int? minQty, string? sort = "name")
        {
            var q = _context.Inventory.AsQueryable();

            // search by name (user story)
            if (!string.IsNullOrWhiteSpace(search))
            {
                var s = search.Trim();
                q = q.Where(x => x.Name.Contains(s));
            }

            // optional min qty filter
            if (minQty.HasValue) q = q.Where(x => x.Quantity >= minQty.Value);

            // sort: id | id_desc | name | name_desc | qty | qty_desc
            q = sort switch
            {
                "id" => q.OrderBy(x => x.ItemId),
                "id_desc" => q.OrderByDescending(x => x.ItemId),
                "qty" => q.OrderBy(x => x.Quantity),
                "qty_desc" => q.OrderByDescending(x => x.Quantity),
                "name_desc" => q.OrderByDescending(x => x.Name),
                _ => q.OrderBy(x => x.Name)
            };

            var items = await q.AsNoTracking().ToListAsync();   // <— always a list (possibly empty)
            ViewData["Search"] = search;
            ViewData["MinQty"] = minQty;
            ViewData["Sort"] = sort;
            return View(items);                                  // <— IMPORTANT: pass the model
        }

        // GET /liam/details/123
        [HttpGet("details/{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var item = await _context.Inventory.FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null) return NotFound();
            return View(item); // Views/Items_Liam/Details.cshtml
        }

        // GET /liam/create
        [HttpGet("create")]
        public IActionResult Create() => View(); // Views/Items_Liam/Create.cshtml

        // POST /liam/create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,Name,Description,Quantity,Notes,IconUrl")] InventoryItem item)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(item.IconUrl) && item.ItemId > 0)
                    item.IconUrl = OsrsIcon.For(item.ItemId);

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET /liam/edit/123
        [HttpGet("edit/{id:int}")]
        public async Task<IActionResult> Edit(int id)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item == null) return NotFound();
            return View(item); // Views/Items_Liam/Edit.cshtml
        }

        // POST /liam/edit/123
        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,Name,Description,Quantity,Notes,IconUrl")] InventoryItem item)
        {
            if (id != item.ItemId) return NotFound();

            if (ModelState.IsValid)
            {
                if (string.IsNullOrWhiteSpace(item.IconUrl) && item.ItemId > 0)
                    item.IconUrl = OsrsIcon.For(item.ItemId);

                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _context.Inventory.AnyAsync(e => e.ItemId == id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET /liam/delete/123
        [HttpGet("delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _context.Inventory.FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null) return NotFound();
            return View(item); // Views/Items_Liam/Delete.cshtml
        }

        // POST /liam/delete/123
        [HttpPost("delete/{id:int}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Inventory.FindAsync(id);
            if (item != null)
            {
                _context.Inventory.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
