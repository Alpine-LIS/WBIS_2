using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ListUpdates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "place_holder",
                table: "wildlife_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "place_holder",
                table: "plant_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "place_holder",
                table: "bird_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "place_holder",
                table: "application_users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "place_holder",
                table: "amphibian_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "place_holder",
                table: "wildlife_species");

            migrationBuilder.DropColumn(
                name: "place_holder",
                table: "plant_species");

            migrationBuilder.DropColumn(
                name: "place_holder",
                table: "bird_species");

            migrationBuilder.DropColumn(
                name: "place_holder",
                table: "application_users");

            migrationBuilder.DropColumn(
                name: "place_holder",
                table: "amphibian_species");
        }
    }
}
