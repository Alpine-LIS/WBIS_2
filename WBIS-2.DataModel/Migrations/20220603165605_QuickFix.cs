using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class QuickFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seasonality_if_flow",
                table: "amphibian_surveys",
                newName: "seasonality_of_flow");

            migrationBuilder.AlterColumn<string>(
                name: "bands",
                table: "owl_bandings",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "seasonality_of_flow",
                table: "amphibian_surveys",
                newName: "seasonality_if_flow");

            migrationBuilder.AlterColumn<string>(
                name: "bands",
                table: "owl_bandings",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
