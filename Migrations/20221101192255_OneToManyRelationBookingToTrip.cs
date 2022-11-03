using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class OneToManyRelationBookingToTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Destination",
                table: "Bookings",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "End Location",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Start Location",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips",
                column: "Booking ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "End Location",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Start Location",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Bookings",
                newName: "Destination");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips",
                column: "Booking ID",
                unique: true);
        }
    }
}
