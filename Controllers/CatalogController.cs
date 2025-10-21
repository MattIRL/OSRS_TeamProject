using Microsoft.AspNetCore.Mvc;
using OSRS_TeamProject.Services;

[Route("catalog")]
public class CatalogController : Controller
{
    private readonly IItemCatalog _catalog;
    public CatalogController(IItemCatalog catalog) => _catalog = catalog;

    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string q)
        => Json(await _catalog.SearchAsync(q ?? "", 20));

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
        => (await _catalog.GetByIdAsync(id)) is { } item ? Json(item) : NotFound();
}
