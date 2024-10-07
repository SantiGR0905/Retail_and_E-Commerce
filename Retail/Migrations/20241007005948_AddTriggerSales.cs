using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGSales
    ON[Sales]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO SaleHistories(SaleId, SaleDate, StateSale, Direction, Users, Products, Modified, ModifiedBy)
            SELECT i.SaleId, i.SaleDate, i.StateSale, i.Direction, i.UsersUserId, i.ProductsProductId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO SaleHistories(SaleId, SaleDate, StateSale, Direction, Users, Products, Modified, ModifiedBy)
            SELECT d.SaleId, d.SaleDate, d.StateSale, d.Direction, d.UsersUserId, d.ProductsProductId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TGSales;");
        }
    }
}
