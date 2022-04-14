using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class SpellingCorrection_pecies_species : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pecies",
                table: "wildlife_species",
                newName: "species");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "species",
                table: "wildlife_species",
                newName: "pecies");
        }
    }
}
