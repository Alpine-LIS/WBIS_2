using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AmphibianElementFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "point_of_interest",
                table: "amphibian_locations_found");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "point_of_interest",
                table: "amphibian_locations_found",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
