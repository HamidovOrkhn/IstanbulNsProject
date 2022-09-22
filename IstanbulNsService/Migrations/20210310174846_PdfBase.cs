using Microsoft.EntityFrameworkCore.Migrations;

namespace IstanbulNs.Migrations
{
    public partial class PdfBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PDFBase",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    OnlineQueryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PDFBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PDFBase_OnlineQueries_OnlineQueryId",
                        column: x => x.OnlineQueryId,
                        principalTable: "OnlineQueries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PDFBase_OnlineQueryId",
                table: "PDFBase",
                column: "OnlineQueryId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PDFBase");
        }
    }
}
