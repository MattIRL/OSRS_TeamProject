using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OSRS_TeamProject.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    IconUrl = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ItemId);
                });

            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "ItemId", "IconUrl", "Name", "Notes", "Quantity" },
                values: new object[,]
                {
                    { 379, "/img/placeholder-item.svg", "Lobster", "Food for slayer", 12 },
                    { 561, "/img/placeholder-item.svg", "Nature rune", "High alch stack", 150 },
                    { 1127, "/img/placeholder-item.svg", "Rune platebody", "Melee set", 1 },
                    { 1521, "/img/placeholder-item.svg", "Oak logs", "Fletching later", 80 },
                    { 4151, "/img/placeholder-item.svg", "Abyssal whip", "Goal: 70 Attack", 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inventory");
        }
    }
}
