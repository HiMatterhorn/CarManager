using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class StartEndTripTimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "End Timestamp",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Start Timestamp",
                table: "Trips",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "End Timestamp",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Start Timestamp",
                table: "Trips");
        }
    }
}
