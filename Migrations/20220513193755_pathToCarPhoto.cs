using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class pathToCarPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_CarModels_CarVIN",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels");

            migrationBuilder.RenameTable(
                name: "CarModels",
                newName: "Cars");

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "VIN");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Cars_CarVIN",
                table: "Bookings",
                column: "CarVIN",
                principalTable: "Cars",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Cars_CarVIN",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "CarModels");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModels",
                table: "CarModels",
                column: "VIN");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_CarModels_CarVIN",
                table: "Bookings",
                column: "CarVIN",
                principalTable: "CarModels",
                principalColumn: "VIN",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
