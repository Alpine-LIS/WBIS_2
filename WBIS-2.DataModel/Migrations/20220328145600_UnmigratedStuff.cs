using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UnmigratedStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "manual_pass_changed",
                table: "site_callings",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "record_type",
                table: "site_callings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_findable",
                table: "bird_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_surveyable",
                table: "bird_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "manual_pass_changed",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "record_type",
                table: "site_callings");

            migrationBuilder.DropColumn(
                name: "is_findable",
                table: "bird_species");

            migrationBuilder.DropColumn(
                name: "is_surveyable",
                table: "bird_species");
        }
    }
}
