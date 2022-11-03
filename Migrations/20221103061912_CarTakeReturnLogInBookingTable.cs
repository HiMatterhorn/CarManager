using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class CarTakeReturnLogInBookingTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CarReturnedUTC",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CarTakenUTC",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarReturnedUTC",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "CarTakenUTC",
                table: "Bookings");
        }
    }
}
