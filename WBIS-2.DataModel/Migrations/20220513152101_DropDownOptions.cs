using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class DropDownOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dropdown_options",
                columns: table => new
                {
                    table = table.Column<string>(type: "text", nullable: true),
                    property = table.Column<string>(type: "text", nullable: true),
                    full_text = table.Column<string>(type: "text", nullable: true),
                    selection_text = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dropdown_options");
        }
    }
}
