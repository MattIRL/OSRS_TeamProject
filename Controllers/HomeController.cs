using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OSRS_TeamProject.Data;
using OSRS_TeamProject.Models;
using System.Diagnostics;

namespace OSRS_TeamProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GameInvContext _db;

        public HomeController(ILogger<HomeController> logger, GameInvContext db)
        {
            _logger = logger;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var items = await _db.Inventory.OrderBy(i => i.Name).Take(5).ToListAsync();
            return View(items); // -> Views/Home/Index.cshtml
        }

        [Route("[action]")]
        public IActionResult Privacy() => View();     // -> Views/Home/Privacy.cshtml

        [Route("[action]")]
        public IActionResult ContactUs() => View();   // -> Views/Home/ContactUs.cshtml

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
            => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
