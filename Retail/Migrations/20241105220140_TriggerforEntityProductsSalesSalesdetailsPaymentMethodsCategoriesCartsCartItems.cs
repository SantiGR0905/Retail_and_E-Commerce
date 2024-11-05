using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Retail.Model;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class TriggerforEntityProductsSalesSalesdetailsPaymentMethodsCategoriesCartsCartItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Trigger Products
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
            INSERT INTO ProductHistories(ProductId, ProductName, Description, CreationDate, Price, Active, Image, Stock, Categories, Modified, ModifiedBy)
            SELECT i.ProductId, i.ProductName, i.Description, i.CreationDate, i.Price, i.Active, i.Image, i.Stock, i.CategoriesCategoryId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO ProductHistories(ProductId, ProductName, Description, CreationDate, Price, Active, Image, Stock, Categories, Modified, ModifiedBy)
            SELECT d.ProductId, d.ProductName, d.Description, d.CreationDate, d.Price, d.Active, d.Image, d.Stock, d.CategoriesCategoryId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
            // Trigger Sales
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
            INSERT INTO SaleHistories(SaleId, SaleDate, StateSale, Direction, Users, PaymentMethods, Modified, ModifiedBy)
            SELECT i.SaleId, i.SaleDate, i.StateSale, i.Direction, i.UsersUserId, i.PaymentMethodsPaymentMethodId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO SaleHistories(SaleId, SaleDate, StateSale, Direction, Users, PaymentMethods, Modified, ModifiedBy)
            SELECT d.SaleId, d.SaleDate, d.StateSale, d.Direction, d.UsersUserId, d.PaymentMethodsPaymentMethodId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
            // Trigger Categories
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGCategories
    ON[Categories]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO CategoryHistories(CategoryId, CategoryName, CategoryDescription, Modified, ModifiedBy)
            SELECT i.CategoryId, i.CategoryName, i.CategoryDescription, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO CategoryHistories(CategoryId, CategoryName, CategoryDescription, Modified, ModifiedBy)
            SELECT d.CategoryId, d.CategoryName, d.CategoryDescription, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
            // Trigger Carts
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGCarts
    ON[Carts]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO CartHistories(CartId, Created, IsActive, Users, Modified, ModifiedBy)
            SELECT i.CartId, i.Created, i.IsActive, i.UsersUserId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO CartHistories(CartId, Created, IsActive, Users, Modified, ModifiedBy)
            SELECT d.CartId, d.Created, d.IsActive, d.UsersUserId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
            // Trigger CartItems
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGCartItems
    ON[CartItems]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO CartItemHistories(CartItemId, Quantity, Carts, Products, Modified, ModifiedBy)
            SELECT i.CartItemId, i.Quantity, i.CartsCartId, i.ProductsProductId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO CartItemHistories(CartItemId, Quantity, Carts, Products, Modified, ModifiedBy)
            SELECT d.CartItemId, d.Quantity, d.CartsCartId, d.ProductsProductId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");

            // Trigger PaymentMethods
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGPaymentMethods
    ON[PaymentMethods]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO PaymentMethodHistories(PaymentMethodId, MethodName, DescriptionMethod, Modified, ModifiedBy)
            SELECT i.PaymentMethodId, i.MethodName, i.DescriptionMethod, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO PaymentMethodHistories(PaymentMethodId, MethodName, DescriptionMethod, Modified, ModifiedBy)
            SELECT d.PaymentMethodId, d.MethodName, d.DescriptionMethod, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
            // Trigger SalesDetails
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGSalesDetails
    ON[SalesDetails]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO SaleDetailHistories(SaleDetailId, Quantity, UnitPrice, Sales, Products, Modified, ModifiedBy)
            SELECT i.SaleDetailId, i.Quantity, i.UnitPrice, i.SalesSaleId, i.ProductsProductId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO SaleDetailHistories(SaleDetailId, Quantity, UnitPrice, Sales, Products, Modified, ModifiedBy
            SELECT d.SaleDetailId, d.Quantity, d.UnitPrice, d.SalesSaleId, d.ProductsProductId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
