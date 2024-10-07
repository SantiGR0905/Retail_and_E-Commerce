using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGProducts
    ON[Products]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO ProductHistories(ProductId, ProductName, Description, CreationDate, Active, Model3D, Categories, Modified, ModifiedBy)
            SELECT i.ProductId, i.ProductName, i.Description, i.CreationDate, i.Active, i.Model3D, i.CategoriesCategoryId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO ProductHistories(ProductId, ProductName, Description, CreationDate, Active, Model3D, Categories, Modified, ModifiedBy)
            SELECT d.ProductId, d.ProductName, d.Description, d.CreationDate, d.Active, d.Model3D, d.CategoriesCategoryId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TGProducts;");
        }
    }
}
