using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class kl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LangId",
                table: "ServiceInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceInfo_LangId",
                table: "ServiceInfo",
                column: "LangId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceInfo_Langs_LangId",
                table: "ServiceInfo",
                column: "LangId",
                principalTable: "Langs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceInfo_Langs_LangId",
                table: "ServiceInfo");

            migrationBuilder.DropIndex(
                name: "IX_ServiceInfo_LangId",
                table: "ServiceInfo");

            migrationBuilder.DropColumn(
                name: "LangId",
                table: "ServiceInfo");
        }
    }
}
