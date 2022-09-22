using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class Inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceInfo_ServiceId",
                table: "ServiceInfo");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_ServiceId",
                table: "ServiceInfo",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ServiceInfo_ServiceId",
                table: "ServiceInfo");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_ServiceId",
                table: "ServiceInfo",
                column: "ServiceId",
                unique: true);
        }
    }
}
