using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BotanicalScopingSpecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "botanical_scoping_species",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    botanical_scoping_id = table.Column<Guid>(type: "uuid", nullable: false),
                    plant_species_id = table.Column<Guid>(type: "uuid", nullable: false),
                    exclude = table.Column<bool>(type: "boolean", nullable: false),
                    exclude_text = table.Column<string>(type: "text", nullable: true),
                    exclude_report = table.Column<bool>(type: "boolean", nullable: false),
                    habitat_description = table.Column<string>(type: "text", nullable: true),
                    nddb_habitat_description = table.Column<string>(type: "text", nullable: true),
                    spi_habitat_description = table.Column<string>(type: "text", nullable: true),
                    protection_summary = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_botanical_scoping_species", x => x.guid);
                    table.ForeignKey(
                        name: "FK_botanical_scoping_species_botanical_scopings_botanical_scop~",
                        column: x => x.botanical_scoping_id,
                        principalTable: "botanical_scopings",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_botanical_scoping_species_plant_species_plant_species_id",
                        column: x => x.plant_species_id,
                        principalTable: "plant_species",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scoping_species_botanical_scoping_id",
                table: "botanical_scoping_species",
                column: "botanical_scoping_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scoping_species_plant_species_id",
                table: "botanical_scoping_species",
                column: "plant_species_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "botanical_scoping_species");
        }
    }
}
