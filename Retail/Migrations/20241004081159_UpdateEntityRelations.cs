using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEntityRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Sales");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "Users",
                newName: "UserTypesUserTypeId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "CategoriesCategoryId");

            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "PermissionsXUsers",
                newName: "UserTypesUserTypeId");

            migrationBuilder.RenameColumn(
                name: "PermissionId",
                table: "PermissionsXUsers",
                newName: "PermissionsPermissionId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Inventories",
                newName: "ProductsProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypesUserTypeId",
                table: "Users",
                column: "UserTypesUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoriesCategoryId",
                table: "Products",
                column: "CategoriesCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsXUsers_PermissionsPermissionId",
                table: "PermissionsXUsers",
                column: "PermissionsPermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsXUsers_UserTypesUserTypeId",
                table: "PermissionsXUsers",
                column: "UserTypesUserTypeId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsXUsers_Permissions_PermissionsPermissionId",
                table: "PermissionsXUsers",
                column: "PermissionsPermissionId",
                principalTable: "Permissions",
                principalColumn: "PermissionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionsXUsers_UserTypes_UserTypesUserTypeId",
                table: "PermissionsXUsers",
                column: "UserTypesUserTypeId",
                principalTable: "UserTypes",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoriesCategoryId",
                table: "Products",
                column: "CategoriesCategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_UserTypes_UserTypesUserTypeId",
                table: "Users",
                column: "UserTypesUserTypeId",
                principalTable: "UserTypes",
                principalColumn: "UserTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Products_ProductsProductId",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsXUsers_Permissions_PermissionsPermissionId",
                table: "PermissionsXUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionsXUsers_UserTypes_UserTypesUserTypeId",
                table: "PermissionsXUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoriesCategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_UserTypes_UserTypesUserTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserTypesUserTypeId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoriesCategoryId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsXUsers_PermissionsPermissionId",
                table: "PermissionsXUsers");

            migrationBuilder.DropIndex(
                name: "IX_PermissionsXUsers_UserTypesUserTypeId",
                table: "PermissionsXUsers");

            migrationBuilder.DropIndex(
                name: "IX_Inventories_ProductsProductId",
                table: "Inventories");

            migrationBuilder.RenameColumn(
                name: "UserTypesUserTypeId",
                table: "Users",
                newName: "UserTypeId");

            migrationBuilder.RenameColumn(
                name: "CategoriesCategoryId",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "UserTypesUserTypeId",
                table: "PermissionsXUsers",
                newName: "UserTypeId");

            migrationBuilder.RenameColumn(
                name: "PermissionsPermissionId",
                table: "PermissionsXUsers",
                newName: "PermissionId");

            migrationBuilder.RenameColumn(
                name: "ProductsProductId",
                table: "Inventories",
                newName: "ProductId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Sales",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
