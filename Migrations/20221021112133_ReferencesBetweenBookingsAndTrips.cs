using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class ReferencesBetweenBookingsAndTrips : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Bookings_BookingModelsId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_BookingModelsId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BookingModelsId",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "DamagesDescription",
                table: "Trips",
                newName: "Damages description");

            migrationBuilder.RenameColumn(
                name: "CostsRemarks",
                table: "Trips",
                newName: "Costs remarks");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips",
                column: "Booking ID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Bookings_Booking ID",
                table: "Trips",
                column: "Booking ID",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Bookings_Booking ID",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_Booking ID",
                table: "Trips");

            migrationBuilder.RenameColumn(
                name: "Damages description",
                table: "Trips",
                newName: "DamagesDescription");

            migrationBuilder.RenameColumn(
                name: "Costs remarks",
                table: "Trips",
                newName: "CostsRemarks");

            migrationBuilder.AddColumn<int>(
                name: "BookingModelsId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BookingModelsId",
                table: "Trips",
                column: "BookingModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Bookings_BookingModelsId",
                table: "Trips",
                column: "BookingModelsId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
