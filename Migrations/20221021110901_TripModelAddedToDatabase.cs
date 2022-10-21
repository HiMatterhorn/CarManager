using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class TripModelAddedToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartMileage = table.Column<long>(name: "Start Mileage", type: "bigint", nullable: false),
                    EndMileage = table.Column<long>(name: "End Mileage", type: "bigint", nullable: false),
                    BookingID = table.Column<int>(name: "Booking ID", type: "int", nullable: false),
                    Costs = table.Column<float>(type: "real", nullable: false),
                    CostsRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Inspection = table.Column<bool>(type: "bit", nullable: false),
                    Inspectionremarks = table.Column<string>(name: "Inspection remarks", type: "nvarchar(max)", nullable: true),
                    Damages = table.Column<bool>(type: "bit", nullable: false),
                    DamagesDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BookingModelsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Bookings_BookingModelsId",
                        column: x => x.BookingModelsId,
                        principalTable: "Bookings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BookingModelsId",
                table: "Trips",
                column: "BookingModelsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");
        }
    }
}
