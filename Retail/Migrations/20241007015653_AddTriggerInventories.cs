using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerInventories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGInventories
    ON[Inventories]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO InventoryHistories(InventoryId, Amount, LastUpdate, Products, Modified, ModifiedBy)
            SELECT i.InventoryId, i.Amount, i.LastUpdate, i.ProductsProductId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO InventoryHistories(InventoryId, Amount, LastUpdate, Products, Modified, ModifiedBy)
            SELECT d.InventoryId, d.Amount, d.LastUpdate, d.ProductsProductId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TGInventories;");
        }
    }
}
