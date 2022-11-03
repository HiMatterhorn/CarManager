using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AmiFlota.Migrations
{
    public partial class ProjectCostInTripTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Project",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Project",
                table: "Trips");
        }
    }
}
