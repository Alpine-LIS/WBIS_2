using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class NullablePhotos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "site_calling_id",
                table: "pictures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "owl_banding_id",
                table: "pictures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_element_id",
                table: "pictures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_element_id",
                table: "pictures",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures");

            migrationBuilder.AlterColumn<Guid>(
                name: "site_calling_id",
                table: "pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "owl_banding_id",
                table: "pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_element_id",
                table: "pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "amphibian_element_id",
                table: "pictures",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_amphibian_elements_amphibian_element_id",
                table: "pictures",
                column: "amphibian_element_id",
                principalTable: "amphibian_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_botanical_elements_botanical_element_id",
                table: "pictures",
                column: "botanical_element_id",
                principalTable: "botanical_elements",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_owl_bandings_owl_banding_id",
                table: "pictures",
                column: "owl_banding_id",
                principalTable: "owl_bandings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_site_callings_site_calling_id",
                table: "pictures",
                column: "site_calling_id",
                principalTable: "site_callings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
