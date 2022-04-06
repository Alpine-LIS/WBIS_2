using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class FixForeignKeyIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "site_callings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings");

            migrationBuilder.DropForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings");

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "site_callings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "owl_bandings",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "cdfw_spotted_owls",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "watershed_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "quad75_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "hex160_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "district_id",
                table: "amphibian_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_districts_district_id",
                table: "amphibian_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_hex160s_hex160_id",
                table: "amphibian_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_quad75s_quad75_id",
                table: "amphibian_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_amphibian_elements_watersheds_watershed_id",
                table: "amphibian_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_districts_district_id",
                table: "botanical_elements",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_hex160s_hex160_id",
                table: "botanical_elements",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_quad75s_quad75_id",
                table: "botanical_elements",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_watersheds_watershed_id",
                table: "botanical_elements",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_districts_district_id",
                table: "cdfw_spotted_owls",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_hex160s_hex160_id",
                table: "cdfw_spotted_owls",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_quad75s_quad75_id",
                table: "cdfw_spotted_owls",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cdfw_spotted_owls_watersheds_watershed_id",
                table: "cdfw_spotted_owls",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_districts_district_id",
                table: "owl_bandings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_hex160s_hex160_id",
                table: "owl_bandings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_quad75s_quad75_id",
                table: "owl_bandings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_owl_bandings_watersheds_watershed_id",
                table: "owl_bandings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_districts_district_id",
                table: "site_callings",
                column: "district_id",
                principalTable: "districts",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_hex160s_hex160_id",
                table: "site_callings",
                column: "hex160_id",
                principalTable: "hex160s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_quad75s_quad75_id",
                table: "site_callings",
                column: "quad75_id",
                principalTable: "quad75s",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_site_callings_watersheds_watershed_id",
                table: "site_callings",
                column: "watershed_id",
                principalTable: "watersheds",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
