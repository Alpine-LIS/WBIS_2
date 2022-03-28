using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SiteCallingStatusesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "occupancy_status",
                table: "site_callings",
                newName: "spow_occupancy_status");

            migrationBuilder.AddColumn<bool>(
                name: "banding_species",
                table: "bird_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "nesting_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "reproductive_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "spow_occupancy_status",
                columns: table => new
                {
                    status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nesting_status");

            migrationBuilder.DropTable(
                name: "reproductive_status");

            migrationBuilder.DropTable(
                name: "spow_occupancy_status");

            migrationBuilder.DropColumn(
                name: "banding_species",
                table: "bird_species");

            migrationBuilder.RenameColumn(
                name: "spow_occupancy_status",
                table: "site_callings",
                newName: "occupancy_status");
        }
    }
}
