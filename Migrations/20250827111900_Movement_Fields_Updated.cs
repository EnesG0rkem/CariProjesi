using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class Movement_Fields_Updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementCredit",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "MovementDebit",
                table: "Movements",
                newName: "MovementChange");

            migrationBuilder.AddColumn<bool>(
                name: "MovementType",
                table: "Movements",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovementType",
                table: "Movements");

            migrationBuilder.RenameColumn(
                name: "MovementChange",
                table: "Movements",
                newName: "MovementDebit");

            migrationBuilder.AddColumn<decimal>(
                name: "MovementCredit",
                table: "Movements",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
