using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class ActiveHex160AndActiveBotanicalSurveyAreas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "active_botanical_survey_areas",
                schema: "public",
                columns: table => new
                {
                    application_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_survey_area_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_botanical_survey_areas", x => new { x.application_user_id, x.botanical_survey_area_id });
                    table.ForeignKey(
                        name: "FK_active_botanical_survey_areas_application_users_application~",
                        column: x => x.application_user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_active_botanical_survey_areas_botanical_survey_areas_botani~",
                        column: x => x.botanical_survey_area_id,
                        principalTable: "botanical_survey_areas",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "active_hex160s",
                schema: "public",
                columns: table => new
                {
                    application_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active_hex160s", x => new { x.application_user_id, x.hex160_id });
                    table.ForeignKey(
                        name: "FK_active_hex160s_application_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "application_users",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_active_hex160s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_active_botanical_survey_areas_botanical_survey_area_id",
                schema: "public",
                table: "active_botanical_survey_areas",
                column: "botanical_survey_area_id");

            migrationBuilder.CreateIndex(
                name: "IX_active_hex160s_hex160_id",
                schema: "public",
                table: "active_hex160s",
                column: "hex160_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "active_botanical_survey_areas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "active_hex160s",
                schema: "public");
        }
    }
}
