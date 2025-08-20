using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CariProje.Migrations
{
    /// <inheritdoc />
    public partial class entities_updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Accounts_AccountId",
                table: "AccountMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements");

            migrationBuilder.DropIndex(
                name: "IX_AccountMovements_AccountId",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AccountMovements");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Accounts",
                newName: "AccountCode");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "AccountMovements",
                newName: "AccoutMovementId");

            migrationBuilder.AddColumn<string>(
                name: "AccountCode",
                table: "AccountMovements",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements",
                column: "AccoutMovementId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_AccountCode",
                table: "AccountMovements",
                column: "AccountCode");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Accounts_AccountCode",
                table: "AccountMovements",
                column: "AccountCode",
                principalTable: "Accounts",
                principalColumn: "AccountCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccountMovements_Accounts_AccountCode",
                table: "AccountMovements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements");

            migrationBuilder.DropIndex(
                name: "IX_AccountMovements_AccountCode",
                table: "AccountMovements");

            migrationBuilder.DropColumn(
                name: "AccountCode",
                table: "AccountMovements");

            migrationBuilder.RenameColumn(
                name: "AccountCode",
                table: "Accounts",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AccoutMovementId",
                table: "AccountMovements",
                newName: "AccountId");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "AccountMovements",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AccountMovements",
                table: "AccountMovements",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AccountMovements_AccountId",
                table: "AccountMovements",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccountMovements_Accounts_AccountId",
                table: "AccountMovements",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
