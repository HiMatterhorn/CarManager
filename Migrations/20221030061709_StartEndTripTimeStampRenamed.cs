using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class StartEndTripTimeStampRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start Timestamp",
                table: "Trips",
                newName: "Start UTC Time");

            migrationBuilder.RenameColumn(
                name: "End Timestamp",
                table: "Trips",
                newName: "End UTC Time");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start UTC Time",
                table: "Trips",
                newName: "Start Timestamp");

            migrationBuilder.RenameColumn(
                name: "End UTC Time",
                table: "Trips",
                newName: "End Timestamp");
        }
    }
}
