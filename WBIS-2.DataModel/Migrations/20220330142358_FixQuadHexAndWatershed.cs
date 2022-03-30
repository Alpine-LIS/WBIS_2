using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixQuadHexAndWatershed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropIndex(
                name: "IX_watersheds_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropColumn(
                name: "hex160_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s",
                columns: new[] { "quad75_id", "watershed_id" });

            migrationBuilder.CreateTable(
                name: "hex160s_quad75s",
                schema: "public",
                columns: table => new
                {
                    hex160_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quad75_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hex160s_quad75s", x => new { x.hex160_id, x.quad75_id });
                    table.ForeignKey(
                        name: "FK_hex160s_quad75s_hex160s_hex160_id",
                        column: x => x.hex160_id,
                        principalTable: "hex160s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_hex160s_quad75s_quad75s_quad75_id",
                        column: x => x.quad75_id,
                        principalTable: "quad75s",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_hex160s_quad75s_quad75_id",
                schema: "public",
                table: "hex160s_quad75s",
                column: "quad75_id");

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.DropTable(
                name: "hex160s_quad75s",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "hex160_id",
                schema: "public",
                table: "watersheds_quad75s",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_watersheds_quad75s",
                schema: "public",
                table: "watersheds_quad75s",
                columns: new[] { "hex160_id", "quad75_id" });

            migrationBuilder.CreateIndex(
                name: "IX_watersheds_quad75s_quad75_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "quad75_id");

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_quad75s_hex160s_hex160_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_watersheds_quad75s_watersheds_watershed_id",
                schema: "public",
                table: "watersheds_quad75s",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");
        }
    }
}
