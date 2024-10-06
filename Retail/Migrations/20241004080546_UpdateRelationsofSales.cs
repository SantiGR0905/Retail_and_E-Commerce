using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationsofSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UserId1",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Sales",
                newName: "UsersUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_UserId1",
                table: "Sales",
                newName: "IX_Sales_UsersUserId");

            migrationBuilder.AddColumn<int>(
                name: "ProductsProductId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Sales_ProductsProductId",
                table: "Sales",
                column: "ProductsProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Products_ProductsProductId",
                table: "Sales",
                column: "ProductsProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_UsersUserId",
                table: "Sales",
                column: "UsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Products_ProductsProductId",
                table: "Sales");

            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UsersUserId",
                table: "Sales");

            migrationBuilder.DropIndex(
                name: "IX_Sales_ProductsProductId",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "ProductsProductId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UsersUserId",
                table: "Sales",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_UsersUserId",
                table: "Sales",
                newName: "IX_Sales_UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_UserId1",
                table: "Sales",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
