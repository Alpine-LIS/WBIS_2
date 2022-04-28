using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class BotanicalElementUpdate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements");

            migrationBuilder.AlterColumn<Guid>(
                name: "botanical_scoping_id",
                table: "botanical_elements",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_botanical_elements_botanical_scopings_botanical_scoping_id",
                table: "botanical_elements",
                column: "botanical_scoping_id",
                principalTable: "botanical_scopings",
                principalColumn: "guid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
