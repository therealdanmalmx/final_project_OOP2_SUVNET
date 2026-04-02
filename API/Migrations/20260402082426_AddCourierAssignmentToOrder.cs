using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddCourierAssignmentToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "CourierIsAssigned",
                table: "Orders",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql(@"
                CREATE TABLE IF NOT EXISTS ""Reviews"" (
                    ""Id"" uuid NOT NULL,
                    ""Score"" real NOT NULL,
                    ""Comment"" text NOT NULL,
                    ""OrderId"" uuid,
                    ""RestaurantId"" uuid,
                    CONSTRAINT ""PK_Reviews"" PRIMARY KEY (""Id"")
                );
            ");

            migrationBuilder.Sql(@"
                CREATE UNIQUE INDEX IF NOT EXISTS ""IX_Reviews_OrderId"" ON ""Reviews"" (""OrderId"");
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropColumn(
                name: "CourierIsAssigned",
                table: "Orders");
        }
    }
}
