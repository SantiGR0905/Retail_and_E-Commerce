using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInventoriesandProductDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductsProductId",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductsProductId",
                table: "Inventories");

            migrationBuilder.DropColumn(
                name: "Products",
                table: "InventoryHistories");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Inventories");

            migrationBuilder.AddColumn<int>(
                name: "InventoriesInventoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Inventories",
                table: "ProductHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Products_InventoriesInventoryId",
                table: "Products",
                column: "InventoriesInventoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Inventories_InventoriesInventoryId",
                table: "Products",
                column: "InventoriesInventoryId",
                principalTable: "Inventories",
                principalColumn: "InventoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Inventories_InventoriesInventoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_InventoriesInventoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "InventoriesInventoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Inventories",
                table: "ProductHistories");

            migrationBuilder.AddColumn<string>(
                name: "Products",
                table: "InventoryHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Inventories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_ProductsProductId",
                table: "Inventories",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Products_ProductsProductId",
                table: "Inventories",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
