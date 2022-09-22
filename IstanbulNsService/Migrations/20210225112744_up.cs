using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class up : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoStr_Doctors_DoctorId",
                table: "InfoStr");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoStr_Langs_LangId",
                table: "InfoStr");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoStr",
                table: "InfoStr");

            migrationBuilder.RenameTable(
                name: "InfoStr",
                newName: "InfoStrs");

            migrationBuilder.RenameIndex(
                name: "IX_InfoStr_LangId",
                table: "InfoStrs",
                newName: "IX_InfoStrs_LangId");

            migrationBuilder.RenameIndex(
                name: "IX_InfoStr_DoctorId",
                table: "InfoStrs",
                newName: "IX_InfoStrs_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoStrs",
                table: "InfoStrs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoStrs_Doctors_DoctorId",
                table: "InfoStrs",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoStrs_Langs_LangId",
                table: "InfoStrs",
                column: "LangId",
                principalTable: "Langs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfoStrs_Doctors_DoctorId",
                table: "InfoStrs");

            migrationBuilder.DropForeignKey(
                name: "FK_InfoStrs_Langs_LangId",
                table: "InfoStrs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InfoStrs",
                table: "InfoStrs");

            migrationBuilder.RenameTable(
                name: "InfoStrs",
                newName: "InfoStr");

            migrationBuilder.RenameIndex(
                name: "IX_InfoStrs_LangId",
                table: "InfoStr",
                newName: "IX_InfoStr_LangId");

            migrationBuilder.RenameIndex(
                name: "IX_InfoStrs_DoctorId",
                table: "InfoStr",
                newName: "IX_InfoStr_DoctorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfoStr",
                table: "InfoStr",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InfoStr_Doctors_DoctorId",
                table: "InfoStr",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InfoStr_Langs_LangId",
                table: "InfoStr",
                column: "LangId",
                principalTable: "Langs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
