using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorialEU.Migrations {
    /// <inheritdoc />
    public partial class pepe : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql(
@"CREATE TRIGGER [dbo].[Productos_UPDATE] ON [dbo].[Productos]
    AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF ((SELECT TRIGGER_NESTLEVEL()) > 1) RETURN;

    DECLARE @Id INT

    SELECT @Id = INSERTED.Id
    FROM INSERTED

    UPDATE dbo.Productos
    SET ModifiedAt = GETUTCDATE()
    WHERE Id = @Id
END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {

        }
    }
}
