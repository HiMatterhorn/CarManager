using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class PreparationOfCarInspectionModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Damages",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Damages description",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Inspection",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "Inspection remarks",
                table: "Trips");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Damages",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Damages description",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Inspection",
                table: "Trips",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Inspection remarks",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
