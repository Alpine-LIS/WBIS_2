using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class DeletedGeoFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "point_eometry",
                table: "deleted_geometries",
                newName: "point_geometry");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "point_geometry",
                table: "deleted_geometries",
                newName: "point_eometry");
        }
    }
}
