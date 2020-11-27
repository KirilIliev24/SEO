using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SearchEngine.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keywords",
                columns: table => new
                {
                    Keyword = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keywords", x => x.Keyword);
                });

            migrationBuilder.CreateTable(
                name: "LinkDetails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Link = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Snippet = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LinkTracker",
                columns: table => new
                {
                    Keywords = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LinkTracker", x => new { x.Keywords, x.Link });
                });

            migrationBuilder.CreateTable(
                name: "PositonAndDates",
                columns: table => new
                {
                    PositionAndDateId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Keywords = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Position = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositonAndDates", x => x.PositionAndDateId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keywords");

            migrationBuilder.DropTable(
                name: "LinkDetails");

            migrationBuilder.DropTable(
                name: "LinkTracker");

            migrationBuilder.DropTable(
                name: "PositonAndDates");
        }
    }
}
