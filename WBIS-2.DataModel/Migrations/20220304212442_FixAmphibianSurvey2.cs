using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixAmphibianSurvey2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_surveys_AmphibianSurvey~",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_surveys_AmphibianSur~",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_points_of_interest_AmphibianSurveyGuid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_locations_found_AmphibianSurveyGuid",
                table: "amphibian_locations_found");

            migrationBuilder.DropColumn(
                name: "AmphibianSurveyGuid",
                table: "amphibian_points_of_interest");

            migrationBuilder.DropColumn(
                name: "AmphibianSurveyGuid",
                table: "amphibian_locations_found");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AmphibianSurveyGuid",
                table: "amphibian_points_of_interest",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AmphibianSurveyGuid",
                table: "amphibian_locations_found",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_points_of_interest_AmphibianSurveyGuid",
                table: "amphibian_points_of_interest",
                column: "AmphibianSurveyGuid");

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_locations_found_AmphibianSurveyGuid",
                table: "amphibian_locations_found",
                column: "AmphibianSurveyGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_surveys_AmphibianSurvey~",
                table: "amphibian_locations_found",
                column: "AmphibianSurveyGuid",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_surveys_AmphibianSur~",
                table: "amphibian_points_of_interest",
                column: "AmphibianSurveyGuid",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");
        }
    }
}
