using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class AccountMovement_Renamed_To_Movement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMovements");

            migrationBuilder.CreateTable(
                name: "Movements",
                columns: table => new
                {
                    MovementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
                    MovementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovementDescription = table.Column<string>(type: "TEXT", nullable: true),
                    MovementDebit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MovementCredit = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movements", x => x.MovementId);
                    table.ForeignKey(
                        name: "FK_Movements_Accounts_AccountCode",
                        column: x => x.AccountCode,
                        principalTable: "Accounts",
                        principalColumn: "AccountCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movements_AccountCode",
                table: "Movements",
                column: "AccountCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movements");

            migrationBuilder.CreateTable(
                name: "AccountMovements",
                columns: table => new
                {
                    AccoutMovementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    MovementCredit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovementDebit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MovementDescription = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMovements", x => x.AccoutMovementId);
                    table.ForeignKey(
                        name: "FK_AccountMovements_Accounts_AccountCode",
                        column: x => x.AccountCode,
                        principalTable: "Accounts",
                        principalColumn: "AccountCode");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_AccountCode",
                table: "AccountMovements",
                column: "AccountCode");
        }
    }
}
