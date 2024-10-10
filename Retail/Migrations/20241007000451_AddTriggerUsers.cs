using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Retail.Migrations
{
    /// <inheritdoc />
    public partial class AddTriggerUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
    CREATE OR ALTER TRIGGER TGUsers
    ON[Users]
    AFTER INSERT, UPDATE, DELETE
    AS
    BEGIN
        SET NOCOUNT ON;
    --If there are inserted or updated records
        IF EXISTS(SELECT* FROM inserted)
        BEGIN
            INSERT INTO UserHistories(UserId, FirstName, LastName, Email, Password, Date, UserTypes, Modified, ModifiedBy)
            SELECT i.UserId, i.FirstName, i.LastName, i.Email, i.Password, i.Date, i.UserTypesUserTypeId, GETDATE(),
                   CASE
                       WHEN EXISTS(SELECT * FROM deleted) THEN 'UPDATE'
                       ELSE 'INSERT'
                   END
            FROM inserted i;
    END

    -- If there are deleted records
    IF EXISTS(SELECT * FROM deleted)
        BEGIN
            INSERT INTO UserHistories(UserId, FirstName, LastName, Email, Password, Date, UserTypes, Modified, ModifiedBy)
            SELECT d.UserId, d.FirstName, d.LastName, d.Email, d.Password, d.Date, d.UserTypesUserTypeId, GETDATE(), 'DELETE'
                  FROM deleted d;
    END
END;
    ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER IF EXISTS TGUsers;");
        }
    }
}
