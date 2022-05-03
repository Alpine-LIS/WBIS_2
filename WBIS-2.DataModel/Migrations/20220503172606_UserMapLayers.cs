using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class UserMapLayers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "_delete",
                table: "botanical_scoping_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "date_added",
                table: "botanical_scoping_species",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "date_modified",
                table: "botanical_scoping_species",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "repository",
                table: "botanical_scoping_species",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "botanical_scoping_species",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "user_modified_id",
                table: "botanical_scoping_species",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "user_map_layers",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uuid", nullable: false),
                    application_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    information_type = table.Column<string>(type: "text", nullable: true),
                    visible_layer = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_map_layers", x => x.guid);
                    table.ForeignKey(
                        name: "FK_user_map_layers_application_users_application_user_id",
                        column: x => x.application_user_id,
                        principalTable: "application_users",
                        principalColumn: "guid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scoping_species_user_id",
                table: "botanical_scoping_species",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_botanical_scoping_species_user_modified_id",
                table: "botanical_scoping_species",
                column: "user_modified_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_map_layers_application_user_id",
                table: "user_map_layers",
                column: "application_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species",
                column: "user_id",
                principalTable: "application_users",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species",
                column: "user_modified_id",
                principalTable: "application_users",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_scoping_species_application_users_user_modified_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropTable(
                name: "user_map_layers");

            migrationBuilder.DropIndex(
                name: "IX_botanical_scoping_species_user_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropIndex(
                name: "IX_botanical_scoping_species_user_modified_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "_delete",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "date_added",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "date_modified",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "repository",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "botanical_scoping_species");

            migrationBuilder.DropColumn(
                name: "user_modified_id",
                table: "botanical_scoping_species");
        }
    }
}
