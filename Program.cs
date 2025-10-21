using Microsoft.EntityFrameworkCore;
using OSRS_TeamProject.Data;
using OSRS_TeamProject.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC + EF Core (SQLite)
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GameInvContext>(opt =>
    opt.UseSqlite(builder.Configuration.GetConnectionString("Default") ?? "Data Source=gameinv.db"));

// ---- Catalog wiring ----
var useRemote = builder.Configuration.GetValue<bool>("UseRemoteCatalog", true);

// Base HttpClient (optional if you only use the typed one)
builder.Services.AddHttpClient();

if (useRemote)
{
    // Typed client with required headers
    builder.Services.AddHttpClient<IItemCatalog, RemoteItemCatalog>(client =>
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("OSRS_TeamProject/1.0 (+your-email@example.com)");
        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        client.Timeout = TimeSpan.FromSeconds(30);
    });
}
// -----------------------------------------------

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
