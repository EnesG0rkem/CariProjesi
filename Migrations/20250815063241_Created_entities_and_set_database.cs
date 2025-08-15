using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class Created_entities_and_set_database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountName = table.Column<string>(type: "TEXT", nullable: true),
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
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "AccountMovements",
                columns: table => new
                {
                    MovementId = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    MovementDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MovementDescription = table.Column<string>(type: "TEXT", nullable: true),
                    MovementDebit = table.Column<decimal>(type: "TEXT", nullable: false),
                    MovementCredit = table.Column<decimal>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountMovements", x => x.MovementId);
                    table.ForeignKey(
                        name: "FK_AccountMovements_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_AccountId",
                table: "AccountMovements",
                column: "AccountId");
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
