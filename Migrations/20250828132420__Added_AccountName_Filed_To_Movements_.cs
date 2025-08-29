using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class _Added_AccountName_Filed_To_Movements_ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountName",
                table: "Movements",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountName",
                table: "Movements");
        }
    }
}
