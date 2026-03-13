using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class DecoupleCourierFromAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Couriers_Accounts_AccountId",
                table: "Couriers");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Couriers",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "CourierId",
                table: "Accounts",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CourierId",
                table: "Accounts",
                column: "CourierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Couriers_CourierId",
                table: "Accounts",
                column: "CourierId",
                principalTable: "Couriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Couriers_CourierId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CourierId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "CourierId",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Couriers",
                newName: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Couriers_Accounts_AccountId",
                table: "Couriers",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
