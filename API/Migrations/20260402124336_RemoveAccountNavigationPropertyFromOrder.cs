using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class RemoveAccountNavigationPropertyFromOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId1",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_AccountId1",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Orders",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_AccountId1",
                table: "Orders",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_AccountId1",
                table: "Orders",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
