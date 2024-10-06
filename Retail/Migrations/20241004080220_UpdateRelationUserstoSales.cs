using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelationUserstoSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UserId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sales",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_UserId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Users_UserId1",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "Sales",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sales_UserId1",
                table: "Sales",
                newName: "IX_Sales_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Users_UserId",
                table: "Sales",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
