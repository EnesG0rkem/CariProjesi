using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class cıldırdım : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountCode = table.Column<string>(type: "TEXT", nullable: false),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
                    AccountSurname = table.Column<string>(type: "TEXT", nullable: true),
                    AccountAddress = table.Column<string>(type: "TEXT", nullable: true),
                    AccountDistrict = table.Column<string>(type: "TEXT", nullable: true),
                    AccountCity = table.Column<string>(type: "TEXT", nullable: true),
                    AccountCountry = table.Column<string>(type: "TEXT", nullable: true),
                    AccountPhone = table.Column<string>(type: "TEXT", nullable: true),
                    AccountEmail = table.Column<string>(type: "TEXT", nullable: true),
                    AccountDebit = table.Column<decimal>(type: "TEXT", nullable: false),
                    AccountCredit = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountCode);
                });

            migrationBuilder.CreateTable(
                name: "AccountMovements",
                columns: table => new
                {
                    AccoutMovementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountCode = table.Column<string>(type: "TEXT", nullable: true),
                    MovementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovementDescription = table.Column<string>(type: "TEXT", nullable: true),
                    MovementDebit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MovementCredit = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountMovements");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
