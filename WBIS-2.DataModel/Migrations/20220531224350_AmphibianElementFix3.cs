using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class AmphibianElementFix3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest");

            migrationBuilder.AlterColumn<Guid>(
                name: "other_wildlife_id",
                table: "amphibian_points_of_interest",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_species_id",
                table: "amphibian_locations_found",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found",
                column: "amphibian_species_id",
                principalTable: "amphibian_species",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id",
                principalTable: "amphibian_species",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest");

            migrationBuilder.AlterColumn<Guid>(
                name: "other_wildlife_id",
                table: "amphibian_points_of_interest",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_species_id",
                table: "amphibian_locations_found",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_locations_found_amphibian_species_amphibian_speci~",
                table: "amphibian_locations_found",
                column: "amphibian_species_id",
                principalTable: "amphibian_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_points_of_interest_amphibian_species_other_wildli~",
                table: "amphibian_points_of_interest",
                column: "other_wildlife_id",
                principalTable: "amphibian_species",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
