using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCourierFromAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
