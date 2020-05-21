using Microsoft.EntityFrameworkCore.Migrations;

namespace MoveMoney.API.Migrations
{
    public partial class addedComission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountrySenderId = table.Column<int>(nullable: false),
                    CountryReceiverId = table.Column<int>(nullable: false),
                    CountrySenderName = table.Column<string>(nullable: true),
                    CountryReceiverName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comissions_Countries_CountryReceiverId",
                        column: x => x.CountryReceiverId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comissions_Countries_CountrySenderId",
                        column: x => x.CountrySenderId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ComissionRanges",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ComissionId = table.Column<int>(nullable: true),
                    ComissionMaster = table.Column<int>(nullable: false),
                    MinAmount = table.Column<decimal>(nullable: false),
                    MaxAmount = table.Column<decimal>(nullable: false),
                    Percentage = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComissionRanges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComissionRanges_Comissions_ComissionId",
                        column: x => x.ComissionId,
                        principalTable: "Comissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComissionRanges_ComissionId",
                table: "ComissionRanges",
                column: "ComissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissions_CountryReceiverId",
                table: "Comissions",
                column: "CountryReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Comissions_CountrySenderId",
                table: "Comissions",
                column: "CountrySenderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComissionRanges");

            migrationBuilder.DropTable(
                name: "Comissions");
        }
    }
}
