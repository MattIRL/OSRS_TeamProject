using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OSRS_TeamProject.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToInventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Inventory",
                type: "TEXT",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "ItemId",
                keyValue: 379,
                column: "Description",
                value: "Cooked crustacean; food.");

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "ItemId",
                keyValue: 561,
                column: "Description",
                value: "Rune used for alchemy.");

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "ItemId",
                keyValue: 1127,
                column: "Description",
                value: "Sturdy rune body armour.");

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "ItemId",
                keyValue: 1521,
                column: "Description",
                value: "Logs cut from an oak tree.");

            migrationBuilder.UpdateData(
                table: "Inventory",
                keyColumn: "ItemId",
                keyValue: 4151,
                column: "Description",
                value: "A weapon from the Abyss.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Inventory");
        }
    }
}
