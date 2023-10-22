﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorialEU.Migrations
{
    /// <inheritdoc />
    public partial class agregarEstadoActivoProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Productos");
        }
    }
}
