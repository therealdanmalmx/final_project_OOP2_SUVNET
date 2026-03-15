using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewToRestaurant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIds",
                table: "Couriers");

            migrationBuilder.AddColumn<float>(
                name: "Review",
                table: "Restaurants",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Review",
                table: "Restaurants");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "OrderIds",
                table: "Couriers",
                type: "uuid[]",
                nullable: true);
        }
    }
}
