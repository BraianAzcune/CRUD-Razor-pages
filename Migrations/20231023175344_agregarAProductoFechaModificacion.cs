using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorialEU.Migrations {
    /// <inheritdoc />
    public partial class agregarAProductoFechaModificacion : Migration {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "Productos",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getutcdate()");


        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "Productos");
        }
    }
}
