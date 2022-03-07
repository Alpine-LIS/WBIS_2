using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixAmphibianSurvey3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_surveys_hex160s_hex160_id",
                table: "amphibian_surveys");

            migrationBuilder.DropIndex(
                name: "IX_amphibian_surveys_hex160_id",
                table: "amphibian_surveys");

            migrationBuilder.DropColumn(
                name: "hex160_id",
                table: "amphibian_surveys");

            migrationBuilder.CreateTable(
                name: "amphibian_surveys_hex160s",
                schema: "public",
                columns: table => new
                {
                    amphibian_survey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_amphibian_surveys_hex160s", x => new { x.amphibian_survey_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_hex160s_amphibian_surveys_amphibian_surve~",
                        column: x => x.amphibian_survey_id,
                        principalTable: "amphibian_surveys",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_amphibian_surveys_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_hex160s_hex160_id",
                schema: "public",
                table: "amphibian_surveys_hex160s",
                column: "hex160_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "amphibian_surveys_hex160s",
                schema: "public");

            migrationBuilder.AddColumn<Guid>(
                name: "hex160_id",
                table: "amphibian_surveys",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_amphibian_surveys_hex160_id",
                table: "amphibian_surveys",
                column: "hex160_id");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_surveys_hex160s_hex160_id",
                table: "amphibian_surveys",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
