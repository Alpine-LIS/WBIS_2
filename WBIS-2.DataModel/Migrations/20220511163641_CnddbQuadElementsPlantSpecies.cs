using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class CnddbQuadElementsPlantSpecies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "plant_species_id",
                table: "cnddb_quad_elements",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "plant_species_id",
                table: "cnddb_occurrences",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_quad_elements_plant_species_id",
                table: "cnddb_quad_elements",
                column: "plant_species_id");

            migrationBuilder.CreateIndex(
                name: "IX_cnddb_occurrences_plant_species_id",
                table: "cnddb_occurrences",
                column: "plant_species_id");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements",
                column: "plant_species_id",
                principalTable: "plant_species",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_occurrences_plant_species_plant_species_id",
                table: "cnddb_occurrences");

            migrationBuilder.DropForeignKey(
                name: "FK_cnddb_quad_elements_plant_species_plant_species_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropIndex(
                name: "IX_cnddb_quad_elements_plant_species_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropIndex(
                name: "IX_cnddb_occurrences_plant_species_id",
                table: "cnddb_occurrences");

            migrationBuilder.DropColumn(
                name: "plant_species_id",
                table: "cnddb_quad_elements");

            migrationBuilder.DropColumn(
                name: "plant_species_id",
                table: "cnddb_occurrences");
        }
    }
}
