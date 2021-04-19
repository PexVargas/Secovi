using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImobiliariasCrawler.Main.Migrations
{
    public partial class CreateUrlsProcessadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrlsProcessadas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Url = table.Column<string>(nullable: true),
                    ProcessedAt = table.Column<DateTime>(nullable: false),
                    Spider = table.Column<int>(nullable: false),
                    ArgsJson = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlsProcessadas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrlsProcessadas_Url",
                table: "UrlsProcessadas",
                column: "Url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlsProcessadas");
        }
    }
}
