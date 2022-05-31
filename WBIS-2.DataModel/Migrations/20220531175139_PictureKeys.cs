using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WBIS_2.DataModel.Migrations
{
    public partial class PictureKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_do_monitorings_DOMonitoringGuid",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_ForestCarnivoreCa~",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_ranch_photo_points_RanchPhotoPointGuid",
                table: "pictures");

            migrationBuilder.RenameColumn(
                name: "RanchPhotoPointGuid",
                table: "pictures",
                newName: "ranch_photo_point_id");

            migrationBuilder.RenameColumn(
                name: "ForestCarnivoreCameraStationGuid",
                table: "pictures",
                newName: "forest_carnivore_camera_station_id");

            migrationBuilder.RenameColumn(
                name: "DOMonitoringGuid",
                table: "pictures",
                newName: "do_monitoring_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_RanchPhotoPointGuid",
                table: "pictures",
                newName: "IX_pictures_ranch_photo_point_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_ForestCarnivoreCameraStationGuid",
                table: "pictures",
                newName: "IX_pictures_forest_carnivore_camera_station_id");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_DOMonitoringGuid",
                table: "pictures",
                newName: "IX_pictures_do_monitoring_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_do_monitorings_do_monitoring_id",
                table: "pictures",
                column: "do_monitoring_id",
                principalTable: "do_monitorings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_forest_carnivore_~",
                table: "pictures",
                column: "forest_carnivore_camera_station_id",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_ranch_photo_points_ranch_photo_point_id",
                table: "pictures",
                column: "ranch_photo_point_id",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pictures_do_monitorings_do_monitoring_id",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_forest_carnivore_~",
                table: "pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_pictures_ranch_photo_points_ranch_photo_point_id",
                table: "pictures");

            migrationBuilder.RenameColumn(
                name: "ranch_photo_point_id",
                table: "pictures",
                newName: "RanchPhotoPointGuid");

            migrationBuilder.RenameColumn(
                name: "forest_carnivore_camera_station_id",
                table: "pictures",
                newName: "ForestCarnivoreCameraStationGuid");

            migrationBuilder.RenameColumn(
                name: "do_monitoring_id",
                table: "pictures",
                newName: "DOMonitoringGuid");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_ranch_photo_point_id",
                table: "pictures",
                newName: "IX_pictures_RanchPhotoPointGuid");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_forest_carnivore_camera_station_id",
                table: "pictures",
                newName: "IX_pictures_ForestCarnivoreCameraStationGuid");

            migrationBuilder.RenameIndex(
                name: "IX_pictures_do_monitoring_id",
                table: "pictures",
                newName: "IX_pictures_DOMonitoringGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_do_monitorings_DOMonitoringGuid",
                table: "pictures",
                column: "DOMonitoringGuid",
                principalTable: "do_monitorings",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_forest_carnivore_camera_stations_ForestCarnivoreCa~",
                table: "pictures",
                column: "ForestCarnivoreCameraStationGuid",
                principalTable: "forest_carnivore_camera_stations",
                principalColumn: "guid");

            migrationBuilder.AddForeignKey(
                name: "FK_pictures_ranch_photo_points_RanchPhotoPointGuid",
                table: "pictures",
                column: "RanchPhotoPointGuid",
                principalTable: "ranch_photo_points",
                principalColumn: "guid");
        }
    }
}
