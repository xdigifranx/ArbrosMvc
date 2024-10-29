using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Arbros.Server.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDuracionColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracion",
                table: "Tiempo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "Tiempo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "HoraInicio",
                table: "Tiempo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Duracion",
                table: "Tiempo",
                type: "int",
                nullable: true);
        }
    }
}
