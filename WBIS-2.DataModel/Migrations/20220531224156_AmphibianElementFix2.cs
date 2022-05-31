using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AmphibianElementFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_survey_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_survey_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_amphibian_surveys_amphibian_survey_id",
                table: "amphibian_elements",
                column: "amphibian_survey_id",
                principalTable: "amphibian_surveys",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
